using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resetbutton : MonoBehaviour
{
    public GameObject[] objectsToControl;
    private Vector3[] originalPositions;
    private Quaternion[] originalRotations;
    private Vector3[] originalScales;
    public GameObject[] indicators; 
    void Start()
    {
        // Save the original transforms of all objects in the array
        SaveOriginalTransforms();
    }

    public void TurnOnObjectsAndResetTransforms()
    {
        // Activate all objects in the array
        foreach (GameObject obj in objectsToControl)
        {
            obj.SetActive(true);
        }

        // Reset the transforms of all objects to their original values
        for (int i = 0; i < objectsToControl.Length; i++)
        {
            objectsToControl[i].transform.position = originalPositions[i];
            objectsToControl[i].transform.rotation = originalRotations[i];
            objectsToControl[i].transform.localScale = originalScales[i];
        }

        //get lean script to set the pivots
        gameObject.GetComponentInParent<LeanPinchScale>().enabled = true;
        gameObject.GetComponentInParent<LeanTwistRotateAxis>().enabled = true;

        foreach (GameObject obj in objectsToControl)
        {
            obj.GetComponent<LeanPinchScale>().enabled = false;
           obj.GetComponent<LeanTwistRotateAxis>().enabled = false;
        }

      

        foreach (GameObject obj in indicators)
        {
            obj.SetActive(true);
        }

        gameObject.SetActive(false);

    }

    private void SaveOriginalTransforms()
    {
        originalPositions = new Vector3[objectsToControl.Length];
        originalRotations = new Quaternion[objectsToControl.Length];
        originalScales = new Vector3[objectsToControl.Length];

        for (int i = 0; i < objectsToControl.Length; i++)
        {
            originalPositions[i] = objectsToControl[i].transform.position;
            originalRotations[i] = objectsToControl[i].transform.rotation;
            originalScales[i] = objectsToControl[i].transform.localScale;

        }
    }
   
}
