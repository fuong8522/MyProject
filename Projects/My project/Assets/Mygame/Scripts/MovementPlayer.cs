using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 10;
    public FloatingJoystick joyStick;
    
    private Animator animator;

    public Transform cameraManager;
    public float rotationSpeed = 3.5f;
    private Quaternion targetRotation;
    private Quaternion playerRotation;

    //Constrain left right
    private float boundary = 6.5f;

    public float turnSmoothTime = 0.5f;
    private float turnSmoothVelocity;

    void Start()
    {
        animator= GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
        ConstrainMovement();
    }

    public void ConstrainMovement()
    {
        if (transform.position.x > boundary)
        {
            transform.position = new Vector3(boundary, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -boundary)
        {
            transform.position = new Vector3(-boundary, transform.position.y, transform.position.z);
        }
    }
    public void Movement()
    {
        Vector3 movement = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical).normalized;

        if (movement.magnitude >= 0.1f)
        {
            animator.SetBool("Walk", true);

            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cameraManager.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * Time.deltaTime * speed);
        }
        else
        {
            animator.SetBool("Walk", false);
        }

    }

}
