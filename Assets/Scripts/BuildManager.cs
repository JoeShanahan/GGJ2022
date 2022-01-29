using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private GameObject _chosenPrefab;
    [SerializeField] private Material _ghostMaterial;

    private Transform _ghostObject;
    private int _rotation;

    private Camera _mainCam;

    public bool IsBuildMode => _chosenPrefab != null;

    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (_chosenPrefab == null)
            return;


        Vector3 ghostPos = GetGhostPosition();
        _ghostObject.position = ghostPos;

        if (Input.mouseScrollDelta.y > 0)
        {
            _rotation += 90;
            RotateGhost();
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            _rotation -= 90;
            RotateGhost();
        }

        if (Input.GetMouseButtonDown(0))
        {
            PlaceGhostedItem(ghostPos);
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            _chosenPrefab = null;
            return;
        }
    }

    private void RotateGhost()
    {
        if (_ghostObject == null)
            return;

        _rotation = _rotation % 360;

        Vector3 angles = _ghostObject.eulerAngles;
        angles.y = _rotation;
        _ghostObject.eulerAngles = angles;
    }

    public void PlaceGhostedItem(Vector3 pos)
    {
        if (pos.y < 0 || pos.y > 8)
            return;

        GameObject newObj = Instantiate(_chosenPrefab);
        newObj.transform.position = pos;
        newObj.transform.rotation = _ghostObject.transform.rotation;
    }

    public void ActivateBuildMode(GameObject prefab)
    {
        _chosenPrefab = prefab;
        GameObject newObj = Instantiate(_chosenPrefab);
        newObj.GetComponent<Collider>().enabled = false;
        newObj.GetComponent<MeshRenderer>().material = _ghostMaterial;
        _ghostObject = newObj.transform;
        RotateGhost();
    }

    public Vector3 GetGhostPosition()
    {
        Vector3 defaultPos = new Vector3(-999, -999, -999);

        if (PointerManager.IsPointerOverUIObject())
            return defaultPos;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            if (hit.collider.tag == MyTags.BuildableFloor)
            {
                return new Vector3(Mathf.RoundToInt(hit.point.x), Mathf.RoundToInt(hit.point.y) + 0.5f, Mathf.RoundToInt(hit.point.z));
            }
            else if (hit.collider.tag == MyTags.Construct)
            {
                // There might be something on top of the highlighted block, we want to place on THAT
                if (Physics.Raycast(hit.transform.position + new Vector3(0, 10, 0), Vector3.down, out RaycastHit hitTwo, 15))
                {
                    if (hitTwo.collider.tag == MyTags.Construct)
                        return hitTwo.transform.position + Vector3.up;
                }
            }
        }

        return defaultPos;
    }


}
