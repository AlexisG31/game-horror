using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class XPlayerMovement : MonoBehaviour
{
    [SerializeField]
    Camera _mainCam;
    Rigidbody _playerRB;
    public float playerSpeed = 900f;
    private float _maxPlayerSpeed = 900f;
    [SerializeField]
    private float  _sprintSpeedAdd = 100f;
    [SerializeField]
    private float _playerSpeedAirLose = 150f;
    public float playerRotationSpeed = 100f;
    public float distToGround = 1f;
    public float maxHP = 100;
    public float currentHP = 100;
    private bool _grounded;
    private bool _sprinting = false;
    private bool _crouch;
    public float groundDrag = 0.5f;
    //housekeeping
    private float _cameraRotateAngle; 
    private float _oldPlayerMaxSpeed;
    void Start()
    {
        Cursor.visible = false;
        _playerRB = GetComponent<Rigidbody>();
    }
    void Update()
    {
        groundMovement();
        transform.Rotate(0, Input.GetAxis("Mouse X") * Time.deltaTime * playerRotationSpeed, 0);
        _cameraRotateAngle += -Input.GetAxis("Mouse Y") * Time.deltaTime * playerRotationSpeed;
        _cameraRotateAngle = Mathf.Clamp(_cameraRotateAngle, -90, 90);
        _mainCam.transform.localRotation = Quaternion.Euler(_cameraRotateAngle, 0, 0);
        groundCheck();
        if(Mathf.Abs(_oldPlayerMaxSpeed - _maxPlayerSpeed) > 4f)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            playerSpeed = _maxPlayerSpeed;
        }
        _oldPlayerMaxSpeed = _maxPlayerSpeed;
    }
    void groundMovement()
    {
        _playerRB.drag = groundDrag;
        Vector3 moveDir = Vector3.forward * Input.GetAxisRaw("Vertical") + Vector3.right * Input.GetAxisRaw("Horizontal");
        _playerRB.AddRelativeForce(moveDir.normalized * playerSpeed * Time.deltaTime, ForceMode.Force);
        if (Input.GetButtonDown("Crouch"))
        {
            Crouch();
        }
        if (Input.GetButtonDown("Sprint"))
        {
            _maxPlayerSpeed += _sprintSpeedAdd;
            _sprinting = true; 
        }
        if (Input.GetButtonUp("Sprint"))
        {
            _maxPlayerSpeed -= _sprintSpeedAdd;
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
        private IEnumerator SmoothlyLerpMoveSpeed()
    {
        float time = 0;
        float difference = Mathf.Abs(_maxPlayerSpeed - playerSpeed);
        float startValue = playerSpeed;

        while (time < difference)
        {
            playerSpeed = Mathf.Lerp(startValue, _maxPlayerSpeed, 3);
            time += Time.deltaTime;
            yield return null;
        }

        playerSpeed = _maxPlayerSpeed;
    }
}