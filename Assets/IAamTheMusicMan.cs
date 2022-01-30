using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAamTheMusicMan : MonoBehaviour
{

    static IAamTheMusicMan _instance;
    
    // Start is called before the first frame update
    void Start()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
