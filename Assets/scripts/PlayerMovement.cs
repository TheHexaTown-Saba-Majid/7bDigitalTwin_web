using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class PlayerMovement: MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [HideInInspector] public float walkSpeed;
    [HideInInspector] public float sprintSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    // Stairs variables
    public float raycastDistance;
    public float stepHeight;
    public float stepSmoothness;
    private Vector3 targetPosition;

    // Jump through space variables
    public KeyCode spaceJumpKey;
    public float spaceJumpForce;
    public GameObject playerfoot;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void Update()
    {
        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, whatIsGround);

        // Handle player input
        MyInput();

        // Handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        // Handle stairs movement
        StairsCheck();
    }

    private void FixedUpdate()
    {
        // Move the player
        MovePlayer();
    }

    private void MyInput()
    {
        // Get input for movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Jump input
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // Space jump input
        if (Input.GetKey(spaceJumpKey))
        {
            SpaceJump();
        }
    }

    private void MovePlayer()
    {
        // Calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        // On ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        // In air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // Reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Jump force
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void SpaceJump()
    {
        // Reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // Jump through space force
        rb.AddForce(transform.up * spaceJumpForce, ForceMode.Impulse);
    }

    public void StairsCheck()
    {
        // Check for stairs only when not grounded
        if (!grounded)
        {
            Vector3 rayDirection = Camera.main.transform.forward;
            Ray ray = new Ray(playerfoot.transform.position, rayDirection);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                if (hit.collider.CompareTag("Stairs"))
                {
                    // Calculate the target position for smooth transition
                    Vector3 targetPosition = new Vector3(transform.position.x, hit.point.y + stepHeight, transform.position.z);

                    Debug.Log("object name: " + hit.collider.gameObject.name);

                    // Smoothly move the player towards the target position
                    transform.position = Vector3.Lerp(transform.position, targetPosition, stepSmoothness * Time.deltaTime);
                }
            }
        }
    }

}
