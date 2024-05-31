using UnityEngine;
using UnityEngine.UI;

public class SelectedButtonFunctionality : MonoBehaviour
{
    public Button[] buttons;
    private void Start()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button));
        }
    }
    private void OnButtonClicked(Button clickedButton)
    {
        foreach (Button button in buttons)
        {
            Image childImage = button.GetComponentInChildren<Image>();
            if (button == clickedButton)
            {
                childImage.enabled = true;
            }
            else
            {
                childImage.enabled = false;
            }
        }
    }
}
