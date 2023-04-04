using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementPlayer : MonoBehaviour
{
    private static MovementPlayer instance = null;
    public static MovementPlayer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MovementPlayer>();
            }
            return instance;
        }
    }

    public GameObject uiPunch;
    private CharacterController characterController;
    public float speed = 10;
    public FloatingJoystick joyStick;

    private Animator animator;
    public bool punch = false;

    public GameObject baseBall;

    public Transform cameraManager;
    public float rotationSpeed = 3.5f;
    private Quaternion targetRotation;
    private Quaternion playerRotation;

    //Constrain left right
    private float boundaryLeftRight = 6.5f;
    private float boundaryUPDown = 0.2f;

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
        //Giới hạn trái phải
        if (transform.position.x > boundaryLeftRight)
        {
            transform.position = new Vector3(boundaryLeftRight, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -boundaryLeftRight)
        {
            transform.position = new Vector3(-boundaryLeftRight, transform.position.y, transform.position.z);
        }
        //Giới hạn trên dưới
        if (transform.position.y > -boundaryUPDown)
        {
            transform.position = new Vector3(transform.position.x,-boundaryUPDown, transform.position.z);
        }
        if (transform.position.y < -boundaryUPDown)
        {
            transform.position = new Vector3(transform.position.x,-boundaryUPDown, transform.position.z);
        }
    }
    public void Movement()
    {
        Vector3 movement = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical).normalized;

        if (movement.magnitude >= 0.1f)
        {
            OnWalkTrue();
            //Tính góc xoay
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cameraManager.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle, 0f);
            //Xoay theo hướng di chuyển
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * Time.deltaTime * speed);
        }
        else
        {
            OnWalkFalse();
        }
        OnIdle();
    }

    public void OnPunchButton()
    {
        animator.SetTrigger("Punch");
        StartCoroutine(OnOffAnimationZombie());
    }

    public void OnIdle()
    {
        animator.SetBool("Idle", true);
    }
    public void OnWalkTrue()
    {
        animator.SetBool("Walk", true);
    }

    public void OnWalkFalse()
    {
        animator.SetBool("Walk", false);
    }
    IEnumerator OnOffAnimationZombie()
    {
        baseBall.SetActive(true);
        uiPunch.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        uiPunch.SetActive(false);
    }

}
