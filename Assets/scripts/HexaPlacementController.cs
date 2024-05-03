using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class HexaPlacementController : MonoBehaviour
{
    public GameObject objectToSpawn;
    private ARRaycastManager arRaycastManager;
    private ARSessionOrigin arSessionOrigin;

    private bool objectSpawned = false;

    private void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        arSessionOrigin = GetComponent<ARSessionOrigin>();
    }

    private void Update()
    {
        if (!objectSpawned && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            if (arRaycastManager.Raycast(ray, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                Instantiate(objectToSpawn, hitPose.position, hitPose.rotation);
                objectSpawned = true; // Set the flag to true after the object is instantiated
            }
        }
    }
}
