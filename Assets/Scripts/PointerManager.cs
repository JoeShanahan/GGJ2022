using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PointerManager : MonoBehaviour
{
    private static Vector3 _mousePos;  // in pixels
    private static bool _isDragging;

    public static bool IsPointerOverUIObject() 
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }

    public static Vector3 MouseOverPosition(LayerMask layer)
    {
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000, layer))
            return hit.point;
        
        return Vector3.zero;
    }
}
