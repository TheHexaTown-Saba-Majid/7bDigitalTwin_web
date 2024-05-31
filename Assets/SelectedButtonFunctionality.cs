using UnityEngine;
using UnityEngine.UI;

public class SelectedButtonFunctionality : MonoBehaviour
{
    public GameObject[] parentGameObjects; // Array of parent GameObjects
    public GameObject[] childGameObjects; // Array of child GameObjects
    private Image[] parentImages; // Array of parent Image components
    private Image[] childImages; // Array of child Image components
    private void Start()
    {
        // Initialize the arrays of parent and child image components
        parentImages = new Image[parentGameObjects.Length];
        childImages = new Image[childGameObjects.Length];
        // Get references to the image components
        for (int i = 0; i < parentGameObjects.Length; i++)
        {
            parentImages[i] = parentGameObjects[i].GetComponent<Image>();
        }
        for (int i = 0; i < childGameObjects.Length; i++)
        {
            childImages[i] = childGameObjects[i].GetComponent<Image>();
        }
    }
    public void OnButtonClick()
    {
        // Toggle the visibility of the parent's image components
        foreach (var parentImage in parentImages)
        {
            parentImage.enabled = !parentImage.enabled;
        }
        // Toggle the visibility of each child's image components
        foreach (var childImage in childImages)
        {
            childImage.enabled = !childImage.enabled;
        }
        // Turn off all other child images in sibling buttons
        var siblingButtons = transform.parent.GetComponentsInChildren<SelectedButtonFunctionality>();
        foreach (var siblingButton in siblingButtons)
        {
            if (siblingButton != this)
            {
                foreach (var siblingChildImage in siblingButton.childImages)
                {
                    siblingChildImage.enabled = false;
                }
                foreach (var siblingParentImage in siblingButton.parentImages)
                {
                    siblingParentImage.enabled = true;
                }
            }
        }
    }
}
