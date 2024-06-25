using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class accordianDropDown : MonoBehaviour, IPointerClickHandler
{
    public RectTransform container;
    public bool isOpen;
    public Button dropdownButton;
    public Animator animator;


    private bool isClicked = false;
    public void OnButtonClick()
    {
        if (!isClicked)
        {
            animator.Play("testing");
        }
        else
        {
            animator.Play("reverse");
        }
        isClicked = !isClicked;
    }


    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
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
        accordianDropDown[] dropdowns = FindObjectsOfType<accordianDropDown>();
        foreach (accordianDropDown dropdown in dropdowns)
        {
            if (dropdown != this)
            {
                dropdown.isOpen = false;
                
                dropdown.SetContainerScale(0);
              //  animator.Play("reverse");

            }
        }
    }
}
