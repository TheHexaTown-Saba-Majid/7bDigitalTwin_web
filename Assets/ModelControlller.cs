using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelControlller : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float zoomSpeed = 10f;
    public float minZoom = 10f;
    public float maxZoom = 100f;
    public Camera mainCamera;
    public Transform cameraParent;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float initialCameraDistance;
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        if (cameraParent == null)
        {
            cameraParent = mainCamera.transform.parent;
        }
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialCameraDistance = Vector3.Distance(transform.position, mainCamera.transform.position);
    }
    void Update()
    {
        RotateModel();
        ZoomModel();
    }
    void RotateModel()
    {
        if (Input.GetMouseButton(0)) // Left mouse button
        {
            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float verticalRotation = -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
            // Rotate around the anchor point
            transform.RotateAround(initialPosition, Vector3.up, horizontalRotation);
            transform.RotateAround(initialPosition, Vector3.right, verticalRotation);
        }
    }
    void ZoomModel()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            Debug.Log("Scroll input detected: " + scroll);
            // Calculate the direction from the camera to the model
            Vector3 direction = (mainCamera.transform.position - transform.position).normalized;
            // Calculate the new distance based on the scroll input
            float currentDistance = Vector3.Distance(transform.position, mainCamera.transform.position);
            float newDistance = Mathf.Clamp(currentDistance - scroll * zoomSpeed, minZoom, maxZoom);
            Debug.Log("Current distance: " + currentDistance);
            Debug.Log("New calculated distance: " + newDistance);
            // Adjust the cameraParent position based on the new distance
            Vector3 newCameraPosition = transform.position + direction * newDistance;
            cameraParent.position = newCameraPosition;
            Debug.Log("New camera parent position: " + cameraParent.position);
        }
    }
}