using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 10;
    public FloatingJoystick joyStick;

    public Transform cameraManager;
    public float rotationSpeed = 3.5f;
    private Quaternion targetRotation;
    private Quaternion playerRotation;

    //Constrain left right
    private float boundary = 6.5f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Movement();
        ConstrainMovement();
    }
    private void FixedUpdate()
    {
        //HandleRotation();
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
        Vector3 movement = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical);

        if (movement.magnitude >= 0.1f)
        {
            transform.forward = movement;
            characterController.Move(movement.normalized * Time.deltaTime * speed);
        }
    }

    public void HandleRotation()
    {
        targetRotation = Quaternion.Euler(0,cameraManager.eulerAngles.y,0);
        playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        Vector3 movement = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical);

        if (movement.magnitude >= 0.1f)
        {
            transform.rotation = playerRotation;
        }

    }
}
