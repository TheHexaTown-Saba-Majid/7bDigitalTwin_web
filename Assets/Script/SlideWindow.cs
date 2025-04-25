using UnityEngine;
using System.Collections;

public class SlideWindow : MonoBehaviour
{
    [Header("Door Settings")]
    public float openPosition = 5f; // Distance to move the door to open
    public float closedPosition = 0f; // Starting position (closed)
    public float openSpeed = 2f; // Speed of opening/closing
    public float delayBeforeClosing = 0.5f; // Delay before closing

    [Header("Door Axis")]
    public bool moveOnX = true; // Move along X-axis
    public bool moveOnY = false; // Move along Y-axis
    public bool moveOnZ = false; // Move along Z-axis

    private bool isOpen = false;
    private bool isMoving = false;
    private Vector3 closedPositionVec;
    private Vector3 openPositionVec;

    void Start()
    {
        // Get initial local position
        Vector3 position = transform.localPosition;

        // Set closed position exactly
        closedPositionVec = position;

        // Apply open position correctly
        if (moveOnX) openPositionVec = new Vector3(closedPosition + openPosition, position.y, position.z);
        if (moveOnY) openPositionVec = new Vector3(position.x, closedPosition + openPosition, position.z);
        if (moveOnZ) openPositionVec = new Vector3(position.x, position.y, closedPosition + openPosition);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen && !isMoving)
        {
            isOpen = true;
            StartCoroutine(MoveDoor(openPositionVec));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOpen && !isMoving)
        {
            StartCoroutine(DelayedClose());
        }
    }

    IEnumerator DelayedClose()
    {
        yield return new WaitForSeconds(delayBeforeClosing); // Prevents instant closing
        isOpen = false;
        StartCoroutine(MoveDoor(closedPositionVec));
    }

    IEnumerator MoveDoor(Vector3 targetPosition)
    {
        isMoving = true; // Lock movement
        float t = 0f;
        Vector3 startPosition = transform.localPosition;

        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        transform.localPosition = targetPosition; // Ensure exact final position
        isMoving = false; // Unlock movement
    }
}
