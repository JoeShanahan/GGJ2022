using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerAnimations : MonoBehaviour
{
    public bool isRunning = false;
    public bool isPointing = false;

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
        if (_rigidBody != null)
            _anim.SetFloat("moveSpeed", _rigidBody.velocity.magnitude);
    
        _anim.SetBool("isRunning", isRunning);
        _anim.SetBool("isPointing", isPointing);
    }

    public void DieForwards()
    {
        _anim.SetBool("isDead", true);
        _anim.ResetTrigger("dieForwardTrigger");
        _anim.SetTrigger("dieForwardTrigger");
    }

    public void DieBackwards()
    {
        _anim.SetBool("isDead", true);
        _anim.ResetTrigger("dieBackwardsTrigger");
        _anim.SetTrigger("dieBackwardsTrigger");
    }
}
