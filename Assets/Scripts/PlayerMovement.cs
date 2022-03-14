using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    public float currentSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _startSpeed;
    private bool _isGrounded;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _layerGround;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _gravity = -9.81f;
    private Vector3 velocity;
    private void Start()
    {
        currentSpeed = _startSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Movement(x,z);

    }
    public void Movement(float forwardInput, float vertical)
    {
        // if (forwardInput != 0)
        // {
        //     currentSpeed += 10 * Time.deltaTime * forwardInput;
        //     currentSpeed = Mathf.Clamp(currentSpeed, -_maxSpeed, _maxSpeed);
        //     Debug.Log(currentSpeed);
        // }
        // else
        // {
        //     if (currentSpeed < 0)
        //     {
        //         currentSpeed += Time.deltaTime;
        //         currentSpeed = Mathf.Clamp(currentSpeed, -_maxSpeed, 0);
        //     }
        //     else if (currentSpeed > 0)
        //     {
        //         currentSpeed -= Time.deltaTime;
        //         currentSpeed = Mathf.Clamp(currentSpeed, 0, _maxSpeed);
        //     }
        // }
        Vector3 move = transform.right * forwardInput + transform.forward * vertical;
        _controller.Move(move * currentSpeed * Time.deltaTime);

        _animator.SetFloat("_speed", Mathf.Abs(vertical));

        _isGrounded = Physics.CheckSphere(_groundCheck.position, 0.3f, _layerGround);

        if (_isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            Jump();
        }
        velocity.y += _gravity * Time.deltaTime;
        _controller.Move(velocity * Time.deltaTime);
    }
    private void Jump()
    {  
    //     _animator.SetBool("isJump", true);
        velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.name);
        if(other.gameObject.CompareTag("ObstanceSmall")){
            _animator.SetTrigger("isFallingForward");
        }
        if(other.gameObject.CompareTag("ObstanceLarger")){
            _animator.SetTrigger("isFallingBackward");
        }
    }
}
