/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class PlacementController : MonoBehaviour
{
    public GameObject objectToSpawn;
    private ARRaycastManager arRaycastManager;
    private ARSessionOrigin arSessionOrigin;

    private void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        arSessionOrigin = GetComponent<ARSessionOrigin>();
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            if (arRaycastManager.Raycast(ray, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                Instantiate(objectToSpawn, hitPose.position, hitPose.rotation);
            }
        }
    }
}*/


/*

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
public class ARObjectSpawner : MonoBehaviour
{
    public GameObject currentArObjectPrefab;
    private ARRaycastManager arRaycastManager;
    private ARSessionOrigin arSessionOrigin;

    private List<ARRaycastHit> hits;

    void Start()
    {
       
        arRaycastManager = GetComponent<ARRaycastManager>();
        arSessionOrigin = GetComponent<ARSessionOrigin>();
    }
    public void ChangeSpawnedObject(GameObject newPrefab)
    {
        // Change the spawned object to the newPrefab
        currentArObjectPrefab = newPrefab;
    }
    
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            if (arRaycastManager.Raycast(ray, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                Instantiate(currentArObjectPrefab, hitPose.position, hitPose.rotation);
            }
        }
    }
}
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARObjectSpawner : MonoBehaviour
{
    public GameObject currentArObjectPrefab;
    private ARRaycastManager arRaycastManager;
    private ARSession arSession;
    public ARPlaneManager arPlaneManager;

    private bool objectSpawned = false;

    void Start()
    {
#if UNITY_EDITOR
        // Check if we are in the Unity Editor
        if (currentArObjectPrefab != null)
        {
            // Instantiate the AR object in the editor
            Instantiate(currentArObjectPrefab, Vector3.zero, Quaternion.identity);
            objectSpawned = true;
        }
#endif
        arRaycastManager = GetComponent<ARRaycastManager>();
        arSession = GetComponent<ARSession>();
    }

    public void ChangeSpawnedObject(GameObject newPrefab)
    {
        // Change the spawned object to the newPrefab
        currentArObjectPrefab = newPrefab;
    }

    private void Update()
    {
        if (!objectSpawned && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Use AR camera for raycasting
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            if (arRaycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                // Adjust the height or rotation if needed
                Instantiate(currentArObjectPrefab, hitPose.position, hitPose.rotation);
                // Stop further object spawning
                objectSpawned = true;

                // Hide the AR planes
                HideARPlanes();

                // Disable AR tracking
                arSession.enabled = false;
            }
        }
    }

    private void HideARPlanes()
    {
        foreach (var plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }
}


