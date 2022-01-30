using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVoice : MonoBehaviour
{
    [SerializeField] GameObject _textBurstPrefab;
    [SerializeField] string[] _getHereText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            foreach (VillagerAI ai in FindObjectsOfType<VillagerAI>())
                ai.GeneratePathToPoint(gameObject.transform.position);
            
            TextBurst burst = W2C.InstantiateAs<TextBurst>(_textBurstPrefab);
            string bark = _getHereText[Random.Range(0, _getHereText.Length)];
            burst.Init(transform.position + Vector3.up, bark);
        }


    }
}
