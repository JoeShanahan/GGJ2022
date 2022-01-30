using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaTower : MonoBehaviour
{
    [SerializeField] Transform _leftRight;
    [SerializeField] Transform _upDown;
    [SerializeField] Villager _currentTarget;
    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] Transform _visualArrow;
    [SerializeField] GameStuff _gameSTate;
    [SerializeField] Collider _triggerField;

    [SerializeField] List<Villager> _possibleTargets = new List<Villager>();
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
        _triggerField.enabled = !_gameSTate.IsNotPlayMode;
        
        if (_gameSTate.IsNotPlayMode)
            return;

        if (_currentTarget != null && _currentTarget.IsDead)
        {
            _currentTarget = null;

            if (_possibleTargets.Contains(_currentTarget))
                _possibleTargets.Remove(_currentTarget);
        }

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
        Villager closest = null;
        float distance = 999;

        foreach (Villager vi in _possibleTargets)
        {
            if (vi == null)
                continue;

            if (vi.tag != MyTags.Villager)
                continue;

            if (vi.IsDead)
                continue;

            float d = Vector3.Distance(transform.position, vi.transform.position);

            if (d < distance)
            {
                distance = d;
                closest = vi;
            }
        }

        _currentTarget = closest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != MyTags.Villager)
            return;

        Villager vi = other.GetComponent<Villager>();

        if (vi == null)
            return;

        if (_possibleTargets.Contains(vi))
            return;

        _possibleTargets.Add(vi);

        if (_currentTarget == null)
            _currentTarget = vi;
    }

    private void OnTriggerExit(Collider other)
    {
        Villager vi = other.GetComponent<Villager>();

        if (vi == null)
            return;

        if (_possibleTargets.Contains(vi))
            _possibleTargets.Remove(vi);

        if (vi == _currentTarget)
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
