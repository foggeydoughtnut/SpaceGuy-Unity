using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    
    

    public float defaultMoveSpeed = 5f;
    public float fastSpeed = 10f;
    public float boosttime;
    [HideInInspector]
    public float boostertime;
    public bool boosting;
    private float movingSpeed;

    public Rigidbody2D rb;
    public Animator animator;
    public Camera cam;

    Vector2 move;
    Vector2 mousePos;
    Vector2 movement;



    private PlayerControls controls;
    [SerializeField] private float gamepadRotateSmoothing = 1000f;
    [SerializeField] private bool isGamepad;
    private CharacterController controller;
    private Vector2 aim;

    
    private PlayerInput playerInput;
    

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        controls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
    }


    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        rb = GetComponent<Rigidbody2D>();
        movingSpeed = defaultMoveSpeed;
        boostertime = 0f;
        boosting = false;
        isGamepad = Gamepad.current != null ? true : false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseManager.paused)
        {
            return;
        }
        isGamepad = Gamepad.current != null ? true : false;

        HandleInput();
        HandleRotation();


        // Rotation
        if (boosting)
        {
            boostertime += Time.deltaTime;
            if (boostertime >= boosttime)
            {
                movingSpeed = defaultMoveSpeed;
                boosting = false;
                boostertime = 0f;
            }
            else
            {
                movingSpeed = fastSpeed;
            }
        }

        movement = controls.Gameplay.Move.ReadValue<Vector2>();
        animator.SetFloat("Speed", Mathf.Max(Mathf.Abs(movement.x), Mathf.Abs(movement.y)));
    }

    private void HandleInput()
    {
        aim = controls.Gameplay.LookDirection.ReadValue<Vector2>();
    }


    private void HandleRotation()
    {
        if (isGamepad)
        {

            Vector2 lookDir = controls.Gameplay.LookDirection.ReadValue<Vector2>();
            
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            if (lookDir != Vector2.zero)
            {
                Quaternion newangle = Quaternion.Euler(0, 0, angle);
                rb.rotation = Quaternion.RotateTowards(Quaternion.Euler(0, 0, rb.rotation), newangle, gamepadRotateSmoothing * Time.deltaTime).eulerAngles.z;


            }
        } else
        {
            Vector2 mousePosition = controls.Gameplay.LookDirection.ReadValue<Vector2>();
            mousePos = cam.ScreenToWorldPoint(mousePosition);
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }
    }

    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * movingSpeed * Time.fixedDeltaTime);
        
    }
    public void OnControlsChanged(PlayerInput input)
    {
        Debug.Log(input.currentControlScheme);
        if (input.currentControlScheme.ToLower() == "controller")
        {
            isGamepad = true;
        }
        else if (input.currentControlScheme.ToLower() == "keyboard and mouse")
        {
            isGamepad = false;
        }


    }

}
