using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController _controller;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _cam;

    private bool _isGrounded;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _layerGround;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity = -9.81f;
    private Vector3 _velocity;

    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _maxSpeed;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    { if(!GameManager.Instance._gameIsStart) return;
        _isGrounded = Physics.CheckSphere(_groundCheck.position, 0.3f, _layerGround);
        PlayerMove();
    }

    public void PlayerMove()
    {
        float horizontal = Input.GetAxis("Horizontal") ;
        float vertical = Input.GetAxis("Vertical") ;
        _animator.SetFloat("left_right", vertical);

        if (horizontal != 0)
        {
            _currentSpeed += 10 * Time.deltaTime * vertical;
            _currentSpeed = Mathf.Clamp(_currentSpeed, -_maxSpeed, _maxSpeed);
        }
        else
        {
            if (_currentSpeed < 0)
            {
                _currentSpeed += Time.deltaTime;
                _currentSpeed = Mathf.Clamp(_currentSpeed, -_maxSpeed, 0);
            }
            else if (_currentSpeed > 0)
            {
                _currentSpeed -= Time.deltaTime;
                _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
            }
        }

        Vector3 moveDir = _cam.transform.right * horizontal * _currentSpeed * Time.deltaTime + _cam.transform.forward * vertical * _currentSpeed * Time.deltaTime;
        moveDir.y = 0f;
        _animator.SetFloat("_speed", _currentSpeed);
        _controller.Move(moveDir);

        
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -1f;
        }
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Jump();
        }
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

        if (moveDir.magnitude != 0f)
        {
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * _cam.GetComponent<CameraMove>()._sensivity * Time.deltaTime);
            Quaternion camRotation = _cam.rotation;
            camRotation.x = 0f;
            camRotation.z = 0f;

            transform.rotation = Quaternion.Lerp(transform.rotation, camRotation, 0.1f);
        }
    }
    private void Jump()
    {
        _animator.SetTrigger("isJump");
        _velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("ObstanceSmall"))
        {
            _animator.SetTrigger("isFallingForward");
        }
        if (other.gameObject.CompareTag("ObstanceLarger"))
        {
            _animator.SetTrigger("isFallingBackward");
        }
    }
}
