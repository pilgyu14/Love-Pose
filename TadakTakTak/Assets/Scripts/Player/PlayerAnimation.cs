using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAnimation : MonoBehaviour
{
    private readonly int _hashJump = Animator.StringToHash("Jump");
    private readonly int _hashX = Animator.StringToHash("Horizontal");
    private readonly int _hashY = Animator.StringToHash("Vertical");
    private readonly int _hashMoveSpeed = Animator.StringToHash("MoveSpeed");

    private readonly int _hashTpose = Animator.StringToHash("TPose");
    private readonly int _hashPushupPose = Animator.StringToHash("PushupPose");
    private readonly int _hashPowerlessPose = Animator.StringToHash("PowerlessPose");

    private readonly int _hashDead = Animator.StringToHash("Die");

    private List<int> _poseList = new List<int>(); 
    private Animator _anim;

    public void Start()
    {
        _anim = GetComponent<Animator>();
        InitPose(); 
    }

    private void InitPose()
    {
        _poseList.Add(_hashTpose);
        _poseList.Add(_hashPushupPose);
        _poseList.Add(_hashPowerlessPose);
    }
    private void CancelAnimate(int exceptionPose)
    {
        for(int i = 0; i < _poseList.Count; i++)
        {
            if (exceptionPose == _poseList[i])
            {
                continue;
            }
            _anim.SetBool(_poseList[i], false); 
        }
    }
    public void AnimateRun(float moveSpeed)
    {
        _anim.SetFloat(_hashMoveSpeed, moveSpeed);
    }
    public void AnimateJump()
    {
        _anim.SetTrigger(_hashJump);
    }
    public void AnimateMove(float x,float y)
    {
        _anim.SetFloat(_hashX, x);
        _anim.SetFloat(_hashY, y);
    }

    public void AnimateTpose(bool isPose)
    {
        Debug.Log("TPose");
        _anim.SetBool(_hashTpose, isPose);
         CancelAnimate(_hashTpose);
    }
    public void AnimatePushupPose(bool isPose)
    {
        Debug.Log("TPushup");
        _anim.SetBool(_hashPushupPose, isPose);
        CancelAnimate(_hashPushupPose);
    }
    public void AnimatePowerlessPose(bool isPose)
    {
        Debug.Log("TPowerless");
        _anim.SetBool(_hashPowerlessPose, isPose);
        CancelAnimate(_hashPowerlessPose);
    }
    
    public void AnimateDie()
    {
        _anim.SetTrigger(_hashDead); 
    }
}
