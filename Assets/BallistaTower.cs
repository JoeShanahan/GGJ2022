using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaTower : MonoBehaviour
{
    [SerializeField] Transform _leftRight;
    [SerializeField] Transform _upDown;
    [SerializeField] Transform _currentTarget;
    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] Transform _visualArrow;

    [SerializeField] List<Transform> _possibleTargets = new List<Transform>();
    Animator _anim;

    [SerializeField] bool _isLoaded;

    public void AnimArrowFire()
    {
        _isLoaded = false;
        _visualArrow.gameObject.SetActive(false);
        GameObject newObj = Instantiate(_arrowPrefab, _visualArrow.transform.position, _visualArrow.transform.rotation);
        
    }

    public void AnimShowArrow()
    {
        _visualArrow.gameObject.SetActive(true);
    }

    public void AnimFullyLoaded()
    {
        _isLoaded = true;
    }

    public void StartArrowFireAnimation()
    {

    }

    void Start()
    {
        _anim = GetComponent<Animator>();
        StartCoroutine(CleanUpJunk());
    }

    void Update()
    {
        if (_currentTarget == null)
        {
            SetTargetAsClosest();
        }


        if (_currentTarget != null)
        {
            AimAtTarget();

            if (_isLoaded)
            {
                _isLoaded = false;
                _anim.ResetTrigger("FireArrow");
                _anim.SetTrigger("FireArrow");
            }
        }
    }

    private void AimAtTarget()
    {
        Vector3 origin = transform.position + Vector3.up;
        Vector3 toTarget = _currentTarget.transform.position - origin;
        toTarget.Normalize();

        _leftRight.rotation = Quaternion.LookRotation(toTarget, Vector3.up) * Quaternion.Euler(-90f, 0f, 0f);
        Vector3 eulerLR = _leftRight.localEulerAngles;
        eulerLR.x = 0;
        eulerLR.y = 0;
        _leftRight.localEulerAngles = eulerLR;

        _upDown.rotation = Quaternion.LookRotation(toTarget, Vector3.up) * Quaternion.Euler(-90f, 0f, 0f);
    }

    private void SetTargetAsClosest()
    {
        Transform closest = null;
        float distance = 999;


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != MyTags.ThePlayer)
            return;

        if (_possibleTargets.Contains(other.transform))
            return;

        _possibleTargets.Add(other.transform);

        if (_currentTarget == null)
            _currentTarget = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (_possibleTargets.Contains(other.transform))
            _possibleTargets.Remove(other.transform);

        if (other.transform == _currentTarget)
        {
            _currentTarget = null;
            SetTargetAsClosest();
        }
    }

    private IEnumerator CleanUpJunk()
    {
        while (true)
        {
            for (int i=_possibleTargets.Count-1; i>=0; i--)
            {
                if (_possibleTargets[i] == null)
                    _possibleTargets.RemoveAt(i);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
