using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 100f;
    public float zoomSpeed = 10f;
    public Camera cam;
    public float minZoomDistance = 2f; 
    public float maxZoomDistance = 20f; 
    private Vector3 lastMousePosition;
    private float currentZoomDistance;
    void Start()
    {
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
            float rotationX = -delta.y * rotationSpeed * Time.deltaTime;
            float rotationY = delta.x * rotationSpeed * Time.deltaTime;
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
              
                target.Rotate(Vector3.up, rotationY, Space.World);
            }
            else
            {
               
                float newRotationZ = target.localEulerAngles.z + rotationX;
                if (newRotationZ > 180) newRotationZ -= 360; 
                newRotationZ = Mathf.Clamp(newRotationZ, -30f, 30f);
                target.localEulerAngles = new Vector3(target.localEulerAngles.x, target.localEulerAngles.y, newRotationZ);
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
            Vector3 direction = (cam.transform.position - target.position).normalized;
            cam.transform.position = target.position + direction * newZoomDistance;
            currentZoomDistance = newZoomDistance;
        }
    }
}