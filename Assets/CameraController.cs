using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class CameraController : MonoBehaviour
{
    public Transform[] targetPositions; // Array to store multiple target positions (GameObjects)
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main; // Assuming your camera is tagged as "MainCamera" in Unity
    }
    public void MoveCameraToPosition(int index)
    {
        if (index < targetPositions.Length)
        {
            Transform target = targetPositions[index];
            if (target != null)
            {
                mainCamera.transform.position = target.position;
                mainCamera.transform.rotation = target.rotation;
            }
            else
            {
                Debug.LogWarning("Target position is null.");
            }
        }
        else
        {
            Debug.LogWarning("Index out of range.");
        }
    }
}