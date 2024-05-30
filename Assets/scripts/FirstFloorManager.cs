using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;

public class FirstFloorManager : MonoBehaviour, IPointerClickHandler
{
    public TMP_Dropdown tmpDropdown; 
    public GameObject[] groundFloor; 
    public Camera[] cameras; 
    public Camera additionalCamera;
    TextMeshProUGUI txt;

    private int check = 0; // Flag to ensure a specific line runs only once

    void Start()
    {
        txt = GameObject.FindGameObjectWithTag("ViewTag").GetComponent<TextMeshProUGUI>();

        if (tmpDropdown != null)
        {
            tmpDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }
        else
        {
            Debug.LogWarning("TMP_Dropdown is not assigned in the inspector.");
        }

        // Deactivate all cameras initially
        DeactivateAllCameras();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        txt.text = gameObject.name;
        // Deactivate all ground floor objects
        for (int i = 0; i < groundFloor.Length; i++)
        {
            if (i == 2)
            {
                groundFloor[i].gameObject.SetActive(true);
            }
            else
            {
                groundFloor[i].gameObject.SetActive(false);
            }
        }

        // Ensure that this block runs only once
        if (check == 0)
        {
            if (cameras.Length > 0 && cameras[0] != null)
            {
                cameras[0].gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Cameras array is either empty or contains null elements.");
            }

            check = 1; // Set check to 1 to prevent this block from running again
        }
    }

    void OnDropdownValueChanged(int value)
    {
        Debug.Log("Selected option: " + tmpDropdown.options[value].text);
        txt.text = tmpDropdown.options[value].text;

        // Deactivate all cameras
        DeactivateAllCameras();

        // Activate the selected camera
        if (value >= 0 && value < cameras.Length && cameras[value] != null)
        {
            cameras[value].gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Selected camera index is out of range or camera is null.");
        }
    }

    void OnDestroy()
    {
        if (tmpDropdown != null)
        {
            tmpDropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
        }
    }

    private void DeactivateAllCameras()
    {
        foreach (var camera in cameras)
        {
            if (camera != null)
            {
                camera.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Camera in the array is null.");
            }
        }
    }
}
