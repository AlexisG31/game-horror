using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class XPlayerMovement : MonoBehaviour
{
    [SerializeField]
    Camera _mainCam;
    Rigidbody _playerRB;
    private float _playerSpeed
    {
        get {
            var crackCocaine = _defaultMoveSpeed * 10f;
            if(_sprinting)
            {
                crackCocaine += _sprintSpeedAdd * 10f;
            }
            else if(_crouch)
            {
                crackCocaine -= _crouchSpeedPenalty * 10f;
            }
            return crackCocaine;
        }
    }
    [SerializeField]
    private float  _sprintSpeedAdd = 5f;
    [SerializeField]
    private float _crouchSpeedPenalty = 5f;
    public float playerRotationSpeed = 100f;
    public float distToGround = 1f;
    public float maxHP = 100;
    public float currentHP = 100;
    private bool _grounded;
    private bool _sprinting = false;
    private bool _crouch;
    //housekeeping
    private float _cameraRotateAngle; 
    [SerializeField]
    private float _defaultMoveSpeed;
    void Start()
    {
        Cursor.visible = false;
        _playerRB = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Movement();
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * playerRotationSpeed, 0);
        _cameraRotateAngle += -Input.GetAxis("Mouse Y") * Time.deltaTime * playerRotationSpeed;
        _cameraRotateAngle = Mathf.Clamp(_cameraRotateAngle, -90, 90);
        _mainCam.transform.localRotation = Quaternion.Euler(_cameraRotateAngle, 0, 0);
        groundCheck();
    }
    void Movement()
    {
        Vector3 moveDir = Vector3.forward * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal");
        _playerRB.velocity = transform.TransformDirection(moveDir.normalized * _playerSpeed * Time.deltaTime);
        if (Input.GetButtonDown("Crouch") && !_sprinting)
        {
            Crouch();
        }
        if (Input.GetButtonDown("Sprint"))
        {
            _sprinting = true; 
            if(_crouch)
            {
                Crouch();
            }
        }
        if (Input.GetButtonUp("Sprint"))
        {
            _sprinting = false;
        }
    }
    void Crouch()
    {
        if(_crouch)
        {
            gameObject.transform.localScale += new Vector3(0, 0.2f, 0);
        }
        else
        {
            gameObject.transform.localScale -= new Vector3(0, 0.2f, 0);
        }
        _crouch = !_crouch;
    }
    void groundCheck()
    {
        _grounded = Physics.Raycast(gameObject.transform.position, -Vector3.up, distToGround+0.1f);
    }
}