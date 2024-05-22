/*using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class TMP_DropdownClickHandler : MonoBehaviour, IPointerClickHandler
{
    public Animator animator; // Reference to the Animator component
    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // Trigger the animation when the dropdown is clicked
        animator.Play("testing");
    }
}

*/
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class TMP_DropdownClickHandler : MonoBehaviour, IPointerClickHandler
{
    public TMP_Dropdown tmpDropdown; // Reference to the TMP_Dropdown component
    public Animator animator; // Reference to the Animator component
    private bool isDropdownOpen = false;
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
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        // Toggle the state of the dropdown
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
        // Check if the dropdown has closed without selecting a value
        if (isDropdownOpen && !tmpDropdown.IsExpanded)
        {
            isDropdownOpen = false;
            animator.Play("reverse");
        }
    }
    void OnDropdownValueChanged(int value)
    {
        // This method will be called whenever a dropdown value is changed
        Debug.Log("Selected option: " + tmpDropdown.options[value].text);
        // Ensure the dropdown closes when a value is selected
        isDropdownOpen = false;
        animator.Play("reverse");
    }
    void OnDestroy()
    {
        // Remove listener to avoid memory leaks
        if (tmpDropdown != null)
        {
            tmpDropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
        }
    }
}