using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMoveComponent : MonoBehaviour, IComponent
{
    [SerializeField]
    private Camera _followCam;
    [SerializeField]
    private float turnSmoothTime = 0.1f;
    [SerializeField]
    private float turnSmoothVelocity;
    private float _speedSmoothVelocity; 

    private float _speedSmoothTime = 0.1f;
    private float airControlPercent = 0.2f;

    private float _currentVelocityY;
    
    [SerializeField]
    private float curSpeed => new Vector2(_characterController.velocity.x,_characterController.velocity.z).magnitude;

    public float CS; 
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _walkSpeed = 3f;
    [SerializeField]
    private float _runSpeed = 8f;
    [SerializeField]
    private float _jumpPower = 5f;
    [SerializeField]
    private float _gravity = -9.8f;
    [SerializeField]
    private float maxDistance = 1.5f;
    [SerializeField]
    private float rotateSpeed = 360f;
    private Vector3 _velocity; 

    private bool isJump = false; // 점프 하는중인가 

    private float _curVelocity => new Vector2(_characterController.velocity.x, _characterController.velocity.y).magnitude; 
    private Camera _mainCam;
    private CharacterController _characterController;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private Vector3 moveDir;
    private Ray ray;
    
    public UnityEvent<float> RunEvent = null; 
    public UnityEvent JumpEvent = null; 
    public UnityEvent<float,float> MoveEvent = null;

    private InputComponent _inputComponent; 
    public void Initialize(InputComponent inputComponent)
    {
        _mainCam = FindObjectOfType<Camera>();
        _characterController = GetComponent<CharacterController>();
        _inputComponent = inputComponent; 
    }
    public void UpdateSomething()
    {
        //_characterController.attachedRigidbody.
        CS = curSpeed;
        InputMove();
        if(_curVelocity > 0.2f )
        {
            Rotate(); 
        }
        if(_inputComponent.IsFire == true)
        {
            RotateCam();
        }
        
    }

    public void Rebound(Vector3 forceDir, float force)
    {
        //Debug.Log("반동");
        Vector3 forceDirection =  transform.TransformDirection(forceDir); 
        _characterController.Move(forceDirection * force); 
    }
    private void InputMove()
    {

        //r = Input.GetAxis("Mouse X");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //_moveSpeed = _runSpeed;
            _moveSpeed = _runSpeed; 
            RunEvent?.Invoke(_moveSpeed); 
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Rotate(); 
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //_moveSpeed = _walkSpeed;
            _moveSpeed = _walkSpeed; 
            RunEvent?.Invoke(_moveSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isJump == false)
        {
            //moveDir.y += _jumpPower;
            _currentVelocityY = _jumpPower; 
            JumpEvent?.Invoke(); 
        }

        Move(_inputComponent.MoveDIr);
    }

    public void Rotate()
    {
        float camRotY =_followCam.transform.eulerAngles.y;
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, camRotY, ref turnSmoothVelocity, turnSmoothTime);

        //float r = Input.GetAxis("Mouse X");
        //transform.Rotate(new Vector3(0,r * rotateSpeed * Time.deltaTime,0)); 
    }
    public void RotateCam()
    {
        Debug.Log("캠회전");
        _followCam.transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(_followCam.transform.eulerAngles.y, transform.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);

        //float r = Input.GetAxis("Mouse X");
        //transform.Rotate(new Vector3(0,r * rotateSpeed * Time.deltaTime,0)); 
    }

    private bool RayGravity()
    {
        ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);

        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        return Physics.Raycast(ray, maxDistance, _layerMask);
    }

    public void ApplyGravity()
    {
        //Debug.Log("IsGrounded" + _characterController.isGrounded);
        //Debug.Log("Ray" + RayGravity());
        //Debug.Log("중력적용중");
        _currentVelocityY += Time.deltaTime * Physics.gravity.y;

        if (_characterController.isGrounded == true || RayGravity() == true)
        {
            isJump = false;
            _currentVelocityY = 0;
        }
        else
        {
            isJump = true;
            //ApplyGravity();
        }
        //_velocity.y += _gravity * Time.deltaTime;
        return;
    }

    private void Move(Vector3 moveDir)
    {
        //if (_characterController.isGrounded == true || RayGravity() == true)
        //{
        //    Debug.Log("플레이어 이동중");
        //    isJump = false; 
        //    moveDir = (transform.right * moveDir.x) + (transform.forward * moveDir.y);
        //}
        //else
        //{
        //    isJump = true;
        //    ApplyGravity();
        //}
        ////      Animate(h, v);

        //_characterController.Move((moveDir.normalized) * _moveSpeed * Time.deltaTime);


        var moveDirection = Vector3.Normalize(transform.forward * moveDir.y + transform.right * moveDir.x);

        var smoothTime = (_characterController.isGrounded == true || RayGravity() == true )? _speedSmoothTime : _speedSmoothTime / airControlPercent;

        _moveSpeed = Mathf.SmoothDamp(_moveSpeed, _moveSpeed, ref _speedSmoothVelocity, smoothTime); //**

        // _currentVelocityY += Time.deltaTime * Physics.gravity.y;
        
        _velocity = moveDirection * _moveSpeed + Vector3.up * _currentVelocityY;

        _characterController.Move(_velocity * Time.deltaTime);

        //if (_characterController.isGrounded == true || RayGravity() == true)
        //{
        //    isJump = false; 
        //    _currentVelocityY = 0;
        //}
        //else
        //{
        //    isJump = true;
        //    //ApplyGravity();
        //}
        MoveEvent?.Invoke(moveDir.x, moveDir.y);

        //Vector3 camrRot = _mainCam.transform.eulerAngles;
        //camrRot.x = 0;
        //_characterController.transform.eulerAngles = camrRot;
    }

}
