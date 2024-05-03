using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Script Called");
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray intersects with a collider
            if (Physics.Raycast(ray, out hit))
            {
                // Do something with the hit information

            }
        }
    }
}