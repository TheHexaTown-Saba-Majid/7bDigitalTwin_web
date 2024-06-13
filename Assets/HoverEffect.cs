using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image popUpImage; // Drag your Image here in the Inspector

    // This method is called when the pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        popUpImage.gameObject.SetActive(true);
    }

    // This method is called when the pointer exits the button
    public void OnPointerExit(PointerEventData eventData)
    {
        popUpImage.gameObject.SetActive(false);
    }
}