using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerAnimation : MonoBehaviour
{
    public float speed = 2.0f; // Adjust the speed of the camera movement
    private Vector3 targetPosition;
    private bool isMoving = false;
    void Start()
    {
        // Initialize the target position to the current camera position
        targetPosition = transform.position;
    }
    void Update()
    {
        // If the camera needs to move, smoothly interpolate towards the target position
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            // Check if the camera has reached the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                isMoving = false;
            }
        }
    }
    public void MoveToPosition(Vector3 newPosition)
    {
        // Set the new target position and flag the camera to start moving
        targetPosition = newPosition;
        isMoving = true;
    }
}
