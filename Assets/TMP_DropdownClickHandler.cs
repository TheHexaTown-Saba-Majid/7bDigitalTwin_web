using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class TMP_DropdownClickHandler : MonoBehaviour, IPointerClickHandler
{
    public TMP_Dropdown tmpDropdown; // Reference to the TMP_Dropdown component
    public Animator animator; // Reference to the Animator component
    private bool isDropdownOpen = false;
    public GameObject[] FirstFloor; // Array of GameObjects to be turned off when dropdown is clicked
    public Camera[] cameras; // Array to hold references to all cameras
    public Camera additionalCamera; // Reference to an additional camera that may need to be turned off
    private int check = 0; // Flag to ensure a specific line runs only once
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (tmpDropdown != null)
        {
            tmpDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }
        // Initially, deactivate all cameras except the first one
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // This block ensures that the code within runs only once
        if (check == 0)
        {
            if (cameras.Length > 0)
            {
                cameras[0].gameObject.SetActive(true);
            }
            check = 1; // Set check to 1 to prevent this block from running again
        }
        for (int i = 0; i < FirstFloor.Length; i++)
        {
            if(i==0)
            {
                FirstFloor[i].gameObject.SetActive(true);
            }
            else
            {
                FirstFloor[i].gameObject.SetActive(false);
            }
        }
           
        isDropdownOpen = !isDropdownOpen;
        if (isDropdownOpen)
        {
            animator.Play("testing");
        }
        else
        {
            animator.Play("reverse");
        }
    }
    void Update()
    {
        if (isDropdownOpen && !tmpDropdown.IsExpanded)
        {
            isDropdownOpen = false;
            animator.Play("reverse");
        }
    }
    void OnDropdownValueChanged(int value)
    {
        Debug.Log("Selected option: " + tmpDropdown.options[value].text);
        // Deactivate all cameras
        foreach (var camera in cameras)
        {
            camera.gameObject.SetActive(false);
        }
        // Deactivate the additional camera if it is on
        if (additionalCamera != null)
        {
            additionalCamera.gameObject.SetActive(false);
        }
        // Activate the selected camera
        if (value >= 0 && value < cameras.Length)
        {
            cameras[value].gameObject.SetActive(true);
        }
        isDropdownOpen = false;
        animator.Play("reverse");
    }
    void OnDestroy()
    {
        if (tmpDropdown != null)
        {
            tmpDropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
        }
    }
}