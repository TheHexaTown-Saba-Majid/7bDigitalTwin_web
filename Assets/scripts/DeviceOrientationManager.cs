using UnityEngine;

public class DeviceOrientationManager : MonoBehaviour
{
    public string tagToFind = "Joystick";
    public GameObject RotationLogPanel;

    private float initialScreenWidth;
    private float initialScreenHeight;

    void Start()
    {
        // Store the initial device resolution
        initialScreenWidth = Screen.width;
        initialScreenHeight = Screen.height;

        // Print the initial device resolution
        Debug.Log($"Initial Device Resolution: {initialScreenWidth}x{initialScreenHeight}");

        // Detect the device and set the orientation accordingly
        DetectDeviceAndSetOrientation();
    }

    public void DetectDeviceAndSetOrientation()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Assume a desktop if resolution is 1920x1080 or greater
        if (screenWidth >= 1920 && screenHeight >= 1080)
        {
            Debug.Log("Desktop detected. No orientation change required.");
            RotationLogPanel.SetActive(false);
            DeactivateJoystick(); // Deactivate joystick for desktop
            return;
        }

        // Example criteria for iPad or large tablet: Adjust as needed
        if ((screenWidth > 1024 && screenHeight > 768) || (screenWidth > 768 && screenHeight > 1024))
        {
            Debug.Log("iPad or large tablet detected");
            RotationLogPanel.SetActive(true);
            ActivateJoystick();
            ForceLandscape();
        }
        // Example criteria for mobile: Adjust as needed
        else if (screenWidth <= 768 || screenHeight <= 768)
        {
            Debug.Log("Mobile device detected");
            RotationLogPanel.SetActive(true);
            ActivateJoystick();
            ForceLandscape();
        }
        else
        {
            Debug.Log("Device not recognized as desktop, mobile, or tablet. Defaulting to landscape.");
            DeactivateJoystick(); // Deactivate joystick if the device doesn't match any criteria
        }
    }
    public void RecheckDeviceOrientation()
    {
        float currentScreenWidth = Screen.width;
        float currentScreenHeight = Screen.height;


        if (currentScreenWidth > currentScreenHeight)
        {
            Debug.Log("Orientation has changed. Activating joystick and disabling RotationLogPanel.");
                          RotationLogPanel.SetActive(false);
                          ActivateJoystick();
        }
        else
        {
            Debug.Log("Device orientation is the same as the initial dimensions. No changes made.");
        }
    }


    private void ActivateJoystick()
    {
        GameObject obj = GameObject.FindGameObjectWithTag(tagToFind);

        if (obj != null)
        {
            obj.SetActive(true); // Activate the GameObject
        }
        else
        {
            Debug.Log($"No GameObject with tag '{tagToFind}' found.");
        }
    }

    private void DeactivateJoystick()
    {
        GameObject obj = GameObject.FindGameObjectWithTag(tagToFind);

        if (obj != null)
        {
            obj.SetActive(false); // Deactivate the GameObject
        }
        else
        {
            Debug.Log($"No GameObject with tag '{tagToFind}' found to deactivate.");
        }
    }
    void ForceLandscape()
    {
        // Force the screen orientation to landscape
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        // Optional: Disable auto-rotation to ensure it stays in landscape
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}
