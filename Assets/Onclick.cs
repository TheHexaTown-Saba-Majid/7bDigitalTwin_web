using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);   
        Debug.Log("Pointer is over UI element.");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        Debug.Log("Pointer is no longer on UI element.");
    }
}
