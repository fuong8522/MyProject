using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementPlayer : MonoBehaviour
{
    public static MovementPlayer instance = null;

    //Biến liên quan đến di chuyển.
    private CharacterController characterController;
    private float speed = 7;
    public FloatingJoystick joyStick;

    public bool checkPunch = true;

    //Biến liên quan đến tấn công.
    public GameObject uiPunch;

    public Transform cameraManager;
    private Animator animator;

    //Giới hạn trên dưới trái phải.
    private float boundaryLeftRight = 6.5f;
    private float boundaryUPDown = 0.2f;

    //Biến liên quan đến xoay player theo hướng di chuyển.
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        checkPunch = false;
    }

    void Update()
    {
        Movement();
        ConstrainMovement();
        CheckAnimationPunch();
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
            transform.position = new Vector3(transform.position.x, -boundaryUPDown, transform.position.z);
        }
        if (transform.position.y < -boundaryUPDown)
        {
            transform.position = new Vector3(transform.position.x, -boundaryUPDown, transform.position.z);
        }
    }
    public void Movement()
    {
        Vector3 movement = new Vector3(joyStick.Horizontal, 0, joyStick.Vertical).normalized;

        if (movement.magnitude >= 0.1f)
        {
            OnWalkTrue();
            //Tính góc xoay
            float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle - 45, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //Xoay theo hướng di chuyển
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle - 45, 0f) * Vector3.forward;
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
        uiPunch.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        uiPunch.SetActive(false);
    }

    void CheckAnimationPunch()
    {
        if (animator.GetCurrentAnimatorStateInfo(1).IsName("m_melee_combat_attack_A") && animator.GetCurrentAnimatorStateInfo(1).normalizedTime < 1)
        {
            checkPunch = true;
        }
        else
        {
            checkPunch = false;

        }
    }
}
