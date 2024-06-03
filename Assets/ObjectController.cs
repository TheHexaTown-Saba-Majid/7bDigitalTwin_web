using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public Transform target; // The 3D object to rotate
    public float rotationSpeed = 100f;
    public float zoomSpeed = 10f;
    public Camera cam; // The camera to zoom
    public float minZoomDistance = 2f; // Minimum zoom distance
    public float maxZoomDistance = 20f; // Maximum zoom distance
    private Vector3 lastMousePosition;
    private float currentZoomDistance;
    void Start()
    {
        // Initialize current zoom distance
        currentZoomDistance = Vector3.Distance(cam.transform.position, target.position);
    }
    void Update()
    {
        RotateObject();
        ZoomCamera();
    }
    void RotateObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePosition;
            float rotationX = delta.y * rotationSpeed * Time.deltaTime;
            float rotationY = -delta.x * rotationSpeed * Time.deltaTime;
            // Check if we should rotate in the X or Y direction
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                target.Rotate(Vector3.up, rotationY, Space.World);
            }
            else
            {
                target.Rotate(Vector3.right, rotationX, Space.World);
            }
            lastMousePosition = Input.mousePosition;
        }
    }
    void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float newZoomDistance = currentZoomDistance - scroll * zoomSpeed;
            newZoomDistance = Mathf.Clamp(newZoomDistance, minZoomDistance, maxZoomDistance);
            // Calculate direction from target to camera
            Vector3 direction = (cam.transform.position - target.position).normalized;
            // Update camera position
            cam.transform.position = target.position + direction * newZoomDistance;
            currentZoomDistance = newZoomDistance;
        }
    }
}