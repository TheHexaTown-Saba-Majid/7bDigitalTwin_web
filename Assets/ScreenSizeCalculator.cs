using UnityEngine;

public class ScreenSizeCalculator : MonoBehaviour
{
    void Start()
    {
        float dpi = Screen.dpi;
        if (Application.isMobilePlatform)
        {
            Debug.Log("Device Type: Mobile");

            if (dpi == 0)
            {
                Debug.Log("DPI information is not available.");
                return;
            }

            float widthInInches = Screen.width / dpi;
            float heightInInches = Screen.height / dpi;

            Debug.Log($"Mobile Screen Size: {widthInInches} x {heightInInches} inches");
        }
        else
        {
            Debug.Log("Device Type: Desktop");

            if (dpi == 0)
            {
                Debug.Log("DPI information is not available.");
                return;
            }

            float widthInInches = Screen.width / dpi;
            float heightInInches = Screen.height / dpi;

            Debug.Log($"Desktop Screen Size: {widthInInches} x {heightInInches} inches");
        }
    }
}
