using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] targetPositions; // Array to store multiple target positions (GameObjects)
    public Camera AnimCam;
    public float animationDuration = 1.0f;

    [SerializeField]
    public Camera groundFloorAnimCamera;
    public Camera FirstFloorAnimCamera;
    public Camera MainCam;

    public GameObject groundfloorRoof;
    public GameObject firstFloorRoof;
    public GameObject GorundFloor;
    public GameObject FirstFloor;

    void Start()
    {
        AnimCam.enabled = false;
        groundFloorAnimCamera.enabled = false;
        FirstFloorAnimCamera.enabled = false;
        MainCam.enabled = true;
    }

    public void MoveCameraToPosition(int index)
    {
        if (index < targetPositions.Length)
        {
            Transform target = targetPositions[index];
            if (target != null)
            {
                SetActiveCamera(AnimCam);
                StopAllCoroutines(); // Stop any ongoing animations
                StartCoroutine(AnimateCameraToPosition(target));
            }
            else
            {
                Debug.LogWarning("Target position is null.");
            }
        }
        else
        {
            Debug.LogWarning("Index out of range.");
        }
    }

    public void GroundFloorManager()
    {
        SetActiveCamera(groundFloorAnimCamera);

        GorundFloor.SetActive(true);
        groundfloorRoof.SetActive(false);
        firstFloorRoof.SetActive(false);
        FirstFloor.SetActive(false);

        // Play the ground floor animation
        groundFloorAnimCamera.GetComponent<Animator>().enabled = true;
    }
    public void FirstFloorManager()
    {
        SetActiveCamera(FirstFloorAnimCamera);

        GorundFloor.SetActive(false);
        groundfloorRoof.SetActive(false);
        firstFloorRoof.SetActive(false);
        FirstFloor.SetActive(true);

        // Play the first floor animation
        FirstFloorAnimCamera.GetComponent<Animator>().enabled = true;
    }

    private void SetActiveCamera(Camera activeCamera)
    {
        AnimCam.enabled = false;
        groundFloorAnimCamera.enabled = false;
        FirstFloorAnimCamera.enabled = false;
        MainCam.enabled = false;

        activeCamera.enabled = true;
    }

    private IEnumerator AnimateCameraToPosition(Transform target)
    {
        Vector3 startPosition = AnimCam.transform.position;
        Quaternion startRotation = AnimCam.transform.rotation;
        Vector3 targetPosition = target.position;
        Quaternion targetRotation = target.rotation;

        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            AnimCam.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / animationDuration);
            AnimCam.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final position and rotation are set correctly
        AnimCam.transform.position = targetPosition;
        AnimCam.transform.rotation = targetRotation;
    }
}
