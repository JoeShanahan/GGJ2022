using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public bool isRunning = false;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private PlayerMove _playerMove;
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
        _anim.SetBool("isGrounded", _playerMove.IsGrounded);
    }

    public void DoJump()
    {
        _anim.ResetTrigger("jumpTrigger");
        _anim.SetTrigger("jumpTrigger");
    }

    public void DoAirJump()
    {
        _anim.ResetTrigger("airJumpTrigger");
        _anim.SetTrigger("airJumpTrigger");
    }
}
