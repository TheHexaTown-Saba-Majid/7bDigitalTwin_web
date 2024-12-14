using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    // Reference to the VirtualJoystick script
    public VirtualJoystick virtualJoystick;

    private float screenWidth;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        screenWidth = Screen.width;

        // Lock cursor (optional)
        // Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }

    void Update()
    {
        // Handle movement and camera control based on touch input
        HandleMovementAndCamera();

        // Apply gravity
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleMovementAndCamera()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.position.x < screenWidth / 2)
            {
                // Left half of the screen: handle player movement
                HandleMovement();
            }
            else
            {
                // Right half of the screen: handle camera movement
                HandleCamera(touch);
            }
        }
        else
        {
            // Fallback to keyboard input for movement
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Keyboard movement
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;

        // Joystick movement (added to the keyboard movement)
        float joystickX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * virtualJoystick.Vertical() : 0;
        float joystickY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * virtualJoystick.Horizontal() : 0;

        // Combine keyboard and joystick input
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * (curSpeedX + joystickX)) + (right * (curSpeedY + joystickY));

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
    }

    void HandleCamera(Touch touch)
    {
        if (canMove)
        {
            rotationX += -touch.deltaPosition.y * lookSpeed * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            float rotationY = touch.deltaPosition.x * lookSpeed * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0, rotationY, 0);
        }
    }
}