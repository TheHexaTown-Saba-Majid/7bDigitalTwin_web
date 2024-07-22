using  UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class firstFloorAccordianDropDown : MonoBehaviour
{
    public RectTransform container;
    public bool isOpen;
    public Button dropdownButton;
    private void Start()
    {
       
        container = transform.Find("Container").GetComponent<RectTransform>();
        if (container == null)
        {
            Debug.LogError("Container not found");
            return;
        }
        dropdownButton = GetComponent<Button>();
        if (dropdownButton == null)
        {
            Debug.LogError("Button component not found");
            return;
        }
        isOpen = false;
        SetContainerScale(0);
    }
    private void Update()
    {
        float targetScale = isOpen ? 1 : 0;
        Vector3 scale = container.localScale;
        scale.y = Mathf.Lerp(scale.y, targetScale, Time.deltaTime * 12);
        container.localScale = scale;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        CloseOtherDropdowns();
        isOpen = !isOpen;
    }
    private void SetContainerScale(float scaleY)
    {
        Vector3 scale = container.localScale;
        scale.y = scaleY;
        container.localScale = scale;
    }
    private void CloseOtherDropdowns()
    {
        firstFloorAccordianDropDown[] dropdowns = FindObjectsOfType<firstFloorAccordianDropDown>();
        foreach (firstFloorAccordianDropDown dropdown in dropdowns)
        {
            if (dropdown != this)
            {
                dropdown.isOpen = false;
                dropdown.SetContainerScale(0);

            }
        }
    }
}
