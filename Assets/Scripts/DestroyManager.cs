using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestroyManager : MonoBehaviour
{
    private bool _isDestroyMode = false;

    private Camera _mainCam;

    public bool IsDestroyMode => _isDestroyMode;

    public void SetDestroyModeActive(bool shouldBeActive)
    {
        if (shouldBeActive == _isDestroyMode)
            return;

        _isDestroyMode = shouldBeActive;
    }

    // Start is called before the first frame update
    void Start()
    {
        _mainCam = Camera.main;
    }


    // Update is called once per frame
    void Update()
    {
        if (_isDestroyMode == false)
            return;

        GameObject hitObj = GetHitObj();

        if (Input.GetMouseButtonDown(0) && hitObj != null)
        {
            DestroyPlacedObject(hitObj);
            return;
        }
    }

    void DestroyPlacedObject(GameObject obj)
    {
        Transform parent = obj.transform.parent;
        int myX = Mathf.RoundToInt(obj.transform.position.x);
        int myZ = Mathf.RoundToInt(obj.transform.position.z);

        float myY = obj.transform.position.y;

        foreach (Transform sibling in parent)
        {
            if (sibling == obj.transform)
                continue;

            int siblingX = Mathf.RoundToInt(sibling.transform.position.x);
            int siblingZ = Mathf.RoundToInt(sibling.transform.position.z);
            float siblingY = sibling.transform.position.y;

            if (myX == siblingX && myZ == siblingZ && myY < siblingY)
            {
                float newY = Mathf.RoundToInt(siblingY - 1.5f) + 0.5f;
                sibling.transform.DOMoveY(newY, 0.5f).SetEase(Ease.OutBounce);
            }
        }

        Destroy(obj);
    }

    public GameObject GetHitObj()
    {
        if (PointerManager.IsPointerOverUIObject())
            return null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            if (hit.collider.tag == MyTags.Construct)
            {
                return hit.collider.gameObject;
            }
        }

        return null;
    }


}
