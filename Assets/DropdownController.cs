using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownManager : MonoBehaviour
{
    public TMP_Dropdown dropdown1;
    public TMP_Dropdown dropdown2;
    public RectTransform dropdown2RectTransform;
    public float dropdown1ExpandedHeight = 200f; // Adjust based on your dropdown height
    public float dropdown1CollapsedHeight = 50f; // Adjust based on your dropdown height
    public float spacing = 10f; // Space between the dropdowns

    private void Start()
    {
        // Add listeners for dropdown events
        dropdown1.onValueChanged.AddListener(delegate { AdjustDropdown2Position(); });

        // Initial adjustment
        AdjustDropdown2Position();
    }

    private void AdjustDropdown2Position()
    {
        // Check if dropdown1 is expanded or collapsed
        bool isDropdown1Expanded = dropdown1.transform.Find("Dropdown List") != null;

        // Adjust the position of dropdown2
        float newY = isDropdown1Expanded ? dropdown1ExpandedHeight + spacing : dropdown1CollapsedHeight + spacing;
        dropdown2RectTransform.anchoredPosition = new Vector2(dropdown2RectTransform.anchoredPosition.x, -newY);
    }

    private void OnDestroy()
    {
        // Remove listeners when the script is destroyed
        dropdown1.onValueChanged.RemoveListener(delegate { AdjustDropdown2Position(); });
    }
}
