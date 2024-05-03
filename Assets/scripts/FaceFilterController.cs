using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FaceFilterController : MonoBehaviour
{
    private ARFaceManager arFaceManager;

    void Start()
    {
        arFaceManager = FindObjectOfType<ARFaceManager>();
        arFaceManager.facesChanged += OnFacesChanged;
    }

    void OnFacesChanged(ARFacesChangedEventArgs args)
    {
        foreach (var face in args.updated)
        {
            UpdateFaceFilterPosition(face);
        }
    }

    void UpdateFaceFilterPosition(ARFace face)
    {
        // Update the position and rotation of your face filter object based on the tracked face's data.
        // Example: transform.position = face.transform.position;
        // Example: transform.rotation = face.transform.rotation;
    }
}
