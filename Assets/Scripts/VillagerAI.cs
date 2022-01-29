using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VillagerAI : MonoBehaviour
{
    [SerializeField] VillagerMove _movement;
    [SerializeField] VillagerAnimations _animation;

    private ActivePath _currentPath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GeneratePathToPoint(FindObjectOfType<PlayerMove>().transform.position);
        }
        
        if (_currentPath != null)
        {
            FollowCurrentPath();
        }
    }

    public void GeneratePathToPoint(Vector3 point)
    {
        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, point,  NavMesh.AllAreas, path);
        
        if (path != null)
            _currentPath = new ActivePath(path);
    }

    public void FollowCurrentPath()
    {
        // TODO Check that it's still valid every second or less
        // * Check the target is still in the same position
        // * CHeck that you've moved recently and aren't stuck
        Vector3 vecToPathFinder = _currentPath.currentPosition -transform.position;
        _movement.inputDirection = Vector3.zero;

        vecToPathFinder.y = 0;

        if (vecToPathFinder.magnitude < 1.25f)
            _currentPath.Advance(_movement.MaxMoveVelocity * Time.deltaTime);

        _currentPath.DrawPath();

        if (_currentPath.IsComplete && vecToPathFinder.magnitude < 0.5f)
        {
            _currentPath = null;
            return;
        }

        Vector2 dirToPath = new Vector2(vecToPathFinder.x, vecToPathFinder.z);
        _movement.inputDirection = dirToPath.normalized;
    }
}

public class ActivePath
{
    public Vector3[] points;
    public Vector3 currentPosition;

    private int _nextPoint = 1;
    private bool _isComplete = false;

    public bool IsComplete => _isComplete;

    private int NextPointIdx => Mathf.Clamp(_nextPoint, 0, points.Length-1);

    private Vector3 NextPoint => points[NextPointIdx];

    public ActivePath(NavMeshPath path)
    {
        currentPosition = path.corners[0];
        points = path.corners;
    }

    public void DrawPath()
    {
        for (int i = 0; i < points.Length - 1; i++)
            Debug.DrawLine(points[i], points[i + 1], Color.blue);

        Debug.DrawLine(currentPosition, currentPosition + Vector3.up, Color.red);
    }

    public void Advance(float distanceRemaining)
    {
        if (_isComplete)
            return;

        // basically a while loop but safer because I don't have time to deal with unity crashing
        for (int i=0; i<128; i++)
        {
            if (_nextPoint > NextPointIdx)
            {
                _isComplete = true;
                break;
            }

            Vector3 vecToNext = NextPoint - currentPosition;

            if (vecToNext.magnitude < distanceRemaining)
            {
                currentPosition = NextPoint;
                distanceRemaining -= vecToNext.magnitude;
                _nextPoint ++;
                continue;
            }

            Vector3 amountToMove = vecToNext.normalized * distanceRemaining;
            currentPosition += amountToMove;
            break;
        }
    }
}
