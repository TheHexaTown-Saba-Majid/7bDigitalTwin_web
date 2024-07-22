using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
public class PersistentDropdown : TMP_Dropdown
{
    // This variable determines whether the dropdown should stay open
    public bool keepDropdownOpen = true;
    protected override GameObject CreateDropdownList(GameObject template)
    {
        // Create the dropdown list object
        GameObject dropdownList = base.CreateDropdownList(template);
        // Add a custom script to the dropdown list to prevent it from closing
        dropdownList.AddComponent<NonClosingDropdownList>().parentDropdown = this;
        return dropdownList;
    }
}
public class NonClosingDropdownList : MonoBehaviour
{
    public PersistentDropdown parentDropdown;
    private void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Get the currently selected item
            var selectedItem = parentDropdown.options[parentDropdown.value].text;
            // Logic to determine whether to close the dropdown or not
            if (!parentDropdown.keepDropdownOpen)
            {
                parentDropdown.Hide();
            }
            else
            {
                Debug.Log("Dropdown remains open. Selected item: " + selectedItem);
            }
        }
    }
}