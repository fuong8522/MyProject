using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 0;
    private Playermain input;
    public FloatingJoystick joyStick;

    private void Awake()
    {
        input = new Playermain();
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
     
        input.Enable();
    }
    private void OnDisable()
    {
        input.Disable();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        Vector3 movement = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical);


        if (movement.magnitude >= 0.1f)
        {
            gameObject.transform.forward = movement;
            characterController.Move(movement.normalized * Time.deltaTime * speed);
        }
    }
}
