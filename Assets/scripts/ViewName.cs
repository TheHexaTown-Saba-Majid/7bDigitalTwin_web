
using UnityEngine;
using UnityEngine.UI;

public class ViewName : MonoBehaviour
{ 
    // Function to handle button click
    public void OnClick()
    {
        string buttonText = "";
        // Check if there's a TextMesh component attached to the button
        TextMesh textMesh = GetComponent<TextMesh>();
        if (textMesh != null)
        {
            buttonText = textMesh.text;
        }
        else
        {
            // If there's no TextMesh component, use the name of the button
            buttonText = gameObject.name;
        }
        // Use buttonText as needed, for example:
        Debug.Log("Button Text: " + buttonText);
    }
}