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
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float initialCameraDistance;
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
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
            float horizontalRotation = -Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
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
            float distance = Vector3.Distance(transform.position, mainCamera.transform.position);
            distance -= scroll * zoomSpeed;
            distance = Mathf.Clamp(distance, minZoom, maxZoom);
            Vector3 direction = (transform.position - mainCamera.transform.position).normalized;
            mainCamera.transform.position = transform.position - direction * distance;
        }
    }
}
