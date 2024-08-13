using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [SerializeField]
    private Camera _playerCamera;
    
    [SerializeField]
    private float _walkSpeed = 5f;
    [SerializeField]
    private float _runSpeed = 10f;
    
    [SerializeField]
    private float _lookSpeed = 2f;
    [SerializeField]
    private float _lookXLimit = 45f;
    
    private Vector3 _moveDirection = Vector3.zero;
    private float _rotationX = 0f;

    private bool _canMove = true;

    private CharacterController _characterController;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!_playerCamera)
        {
            _playerCamera = Camera.main;
        }
        
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canMove)
        {
            return;
        }
        
        var forward = transform.TransformDirection(Vector3.forward);
        var right = transform.TransformDirection(Vector3.right);

        var isRunning = Input.GetKey(KeyCode.LeftShift);
        var currentSpeedX = _canMove ? (isRunning ? _runSpeed : _walkSpeed) * Input.GetAxis("Vertical") : 0f;
        var currentSpeedY = _canMove ? (isRunning ? _runSpeed : _walkSpeed) * Input.GetAxis("Horizontal") : 0f;
        var movementDirectionY = _moveDirection.y;
        _moveDirection = forward * currentSpeedX + right * currentSpeedY;

        _characterController.Move(_moveDirection * Time.deltaTime);

        _rotationX -= Input.GetAxis("Mouse Y") * _lookSpeed;
        _rotationX = Mathf.Clamp(_rotationX, -_lookXLimit, _lookXLimit);
        _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
        transform.rotation *= Quaternion.Euler(0f, Input.GetAxis("Mouse X") * _lookSpeed, 0f);
    }
}
