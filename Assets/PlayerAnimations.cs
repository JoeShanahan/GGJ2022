using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public bool isRunning = false;
    [SerializeField] private Rigidbody _rigidBody;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SyncVars();   
    }

    void SyncVars()
    {
        _anim.SetFloat("moveSpeed", _rigidBody.velocity.magnitude);
        _anim.SetBool("isRunning", isRunning);
    }
}
