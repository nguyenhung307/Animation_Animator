using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;
    [SerializeField] Animator animator;

    float smooth;
    float turnSmootTime = 0.1f;
    private void Start()
    {
        animator.SetFloat("speed", 0);
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        PlayerMove();
    }

    public void PlayerMove(){
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smooth, turnSmootTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            controller.Move(direction * speed * Time.deltaTime);
            Debug.Log(controller.velocity.magnitude);
            
        }
        animator.SetFloat("speed", controller.velocity.magnitude);
    }
}
