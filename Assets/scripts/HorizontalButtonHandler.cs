using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class HorizontalButtonHandler : MonoBehaviour, IPointerClickHandler
{
    public GameObject arObjectPrefab;
    public GameObject spawnController;  // Reference to your AR object spawning controller
    public void OnPointerClick(PointerEventData eventData)
    {
        // Change the game object of the spawn item
        if (spawnController != null)
        {
            spawnController.GetComponent<ARObjectSpawner>().ChangeSpawnedObject(arObjectPrefab);
        }
    }
}