using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
public class TMP_DropdownClickHandler : MonoBehaviour, IPointerClickHandler
{
    public TMP_Dropdown tmpDropdown; 
    public Animator animator; 
    private bool isDropdownOpen = false;
    public GameObject[] FirstFloor; 
    public Camera[] cameras;
    public Camera additionalCamera;
    private int check = 0;
    TextMeshProUGUI txt;

    void Start()
    {
       txt = GameObject.FindGameObjectWithTag("ViewTag").GetComponent<TextMeshProUGUI>();
        
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if (tmpDropdown != null)
        {
            tmpDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        txt.text = gameObject.name;

        if (check == 0)
        {
            if (cameras.Length > 0)
            {
                cameras[0].gameObject.SetActive(true);
            }
            check = 1;
        }
        for (int i = 0; i < FirstFloor.Length; i++)
        {
            if(i==0)
            {
                FirstFloor[i].gameObject.SetActive(true);
            }
            else
            {
                FirstFloor[i].gameObject.SetActive(false);
            }
        }
           
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
        if (isDropdownOpen && !tmpDropdown.IsExpanded)
        {
            isDropdownOpen = false;
            animator.Play("reverse");
        }
    }
    void OnDropdownValueChanged(int value)
    {
        Debug.Log("Selected option: " + tmpDropdown.options[value].text);
       txt.text = tmpDropdown.options[value].text;

        foreach (var camera in cameras)
        {
            camera.gameObject.SetActive(false);
        }
        if (additionalCamera != null)
        {
            additionalCamera.gameObject.SetActive(false);
        }
        if (value >= 0 && value < cameras.Length)
        {
            cameras[value].gameObject.SetActive(true);
        }
        isDropdownOpen = false;
        animator.Play("reverse");
    }
    void OnDestroy()
    {
        if (tmpDropdown != null)
        {
            tmpDropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
        }
    }
}