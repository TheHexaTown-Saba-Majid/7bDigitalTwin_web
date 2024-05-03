using System.Collections;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public float interactionRange = 1f;
    public string interactableTag = "Door";
    public GameObject[] doors; // Array to store multiple doors
    [SerializeField]
    private Animator[] doorAnimators; // Array to store door animators
    [SerializeField]
    private string[] animationStatus;


    void Start()
    {
        doorAnimators = new Animator[doors.Length];
        animationStatus = new string[doors.Length];


        for (int i = 0; i < doors.Length; i++)
        {
            doorAnimators[i] = doors[i].GetComponent<Animator>();
             animationStatus[i] = "DoorClosed";


        }
    }

    private void Update()
    {
        Vector3 rayDirection = Camera.main.transform.forward;
        Ray ray = new Ray(transform.position, rayDirection);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.yellow);

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                Debug.Log("Object name: " + hit.collider.gameObject.name);
                int doorIndex = System.Array.IndexOf(doors, hit.collider.gameObject);
                if (doorIndex != -1)
                {
                    StartCoroutine(TriggerDoorAnimation(doorIndex));
                    // Optionally remove collider for specific doors

                    if (doorIndex == 0 || doorIndex == 3)
                    {
                        if (animationStatus[0] == "DoorOpen" || animationStatus[3] == "DoorOpen")
                        {
                            Collider doorCollider = hit.collider.GetComponent<Collider>();
                            if (doorCollider != null)
                            {
                                Destroy(doorCollider);
                            }
                        }
                    }
                }
            }
        }
    }

    IEnumerator TriggerDoorAnimation(int doorIndex)
    {
        if (doorAnimators[doorIndex] != null)
        {
            doorAnimators[doorIndex].SetTrigger("DoorAnimation");
            yield return new WaitForSeconds(doorAnimators[doorIndex].GetCurrentAnimatorStateInfo(0).length);
            animationStatus[doorIndex] = "DoorOpen";
            Debug.Log("animation finished");
        }
    }
}
