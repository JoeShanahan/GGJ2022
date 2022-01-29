using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationHelper : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            RegenerateNavmesh();
    }
    
    public void RegenerateNavmesh()
    {
        GetComponent<UnityEngine.AI.NavMeshSurface>().BuildNavMesh();
    }
}
