using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public float openAngle = -180f; // Default open angle (can be negative)
    public float closeAngle = -90f; // Default close angle
    public float openSpeed = 2f; // Speed of opening/closing
    public float delayBeforeClosing = 0.5f; // Delay before closing

    [Header("Door Axis")]
    public bool rotateOnX = false; // Rotate around X-axis
    public bool rotateOnY = true; // Rotate around Y-axis (Default)
    public bool rotateOnZ = false; // Rotate around Z-axis

    private bool isOpen = false;
    private bool isMoving = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    void Start()
    {
        // Get initial local rotation
        Vector3 rotation = transform.localEulerAngles;

        // Set closed rotation exactly
        closedRotation = Quaternion.Euler(rotation);

        // Apply open angle correctly
        if (rotateOnX) openRotation = Quaternion.Euler(closeAngle + openAngle, rotation.y, rotation.z);
        if (rotateOnY) openRotation = Quaternion.Euler(rotation.x, closeAngle + openAngle, rotation.z);
        if (rotateOnZ) openRotation = Quaternion.Euler(rotation.x, rotation.y, closeAngle + openAngle);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isOpen && !isMoving)
        {
            isOpen = true;
            StartCoroutine(RotateDoor(openRotation));
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
        StartCoroutine(RotateDoor(closedRotation));
    }

    IEnumerator RotateDoor(Quaternion targetRotation)
    {
        isMoving = true; // Lock movement
        float t = 0f;
        Quaternion startRotation = transform.rotation;

        while (t < 1f)
        {
            t += Time.deltaTime * openSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }

        transform.rotation = targetRotation; // Ensure exact final position
        isMoving = false; // Unlock movement
    }
}
