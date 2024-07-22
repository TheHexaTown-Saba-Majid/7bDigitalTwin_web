using UnityEngine;
using System.Collections.Generic;
public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject firstFloorCamera;
    [SerializeField] private GameObject groundFloorCamera;
    [SerializeField] private List<Transform> groundFloorTransforms; // List of transforms for ground floor camera
    [SerializeField] private List<Transform> firstFloorTransforms; // List of transforms for first floor camera
    [SerializeField] private int buttonCount = 6; // Number of buttons (optional)
    void Start()
    {
        // Ensure all cameras are initially disabled except the main camera
        firstFloorCamera.SetActive(false);
        groundFloorCamera.SetActive(false);
    }
    public void OnButtonClick(int buttonIndex)
    {
        // Validate button index
        if (buttonIndex < 0 || buttonIndex >= buttonCount)
        {
            Debug.LogError("Invalid button index: " + buttonIndex);
            return;
        }
        // Handle ground floor buttons (0-2)
        if (buttonIndex < 3)
        {
            mainCamera.SetActive(false); ;
            firstFloorCamera.SetActive(false);
            groundFloorCamera.SetActive(true);
            // Move ground floor camera to corresponding transform
            groundFloorCamera.transform.position = groundFloorTransforms[buttonIndex].position;
        }
        else // Handle first floor buttons (3-5)
        {
            mainCamera.SetActive(false);
            groundFloorCamera.SetActive(false);
            firstFloorCamera.SetActive(true);
            // Move first floor camera to corresponding transform (button index offset by 3)
            int transformIndex = buttonIndex - 3;
            firstFloorCamera.transform.position = firstFloorTransforms[transformIndex].position;
        }
    }
}









