using Lean.Touch;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    private void OnMouseDown()
    {
         // Get the grandparent GameObject
        GameObject grandparentObject = transform.parent?.parent?.gameObject;
        GameObject parent =  transform.parent?.gameObject;
        // Get the LeanPinchScale component of the grandparent and disable it
        if (grandparentObject != null)
        {
            Lean.Touch.LeanPinchScale leanPinchScale = grandparentObject.GetComponent<Lean.Touch.LeanPinchScale>();
            if (leanPinchScale != null)
            {
                leanPinchScale.enabled = false;
            }
            else
            {
                Debug.LogWarning("LeanPinchScale component not found on the grandparent GameObject.");
            }
        }
        else
        {
            Debug.LogWarning("No grandparent GameObject found.");
        }

        // Get the LeanTwistRotateAxis component of the grandparent and disable it
        Lean.Touch.LeanTwistRotateAxis leanTwistRotateAxis = transform.parent?.parent?.gameObject.GetComponent<Lean.Touch.LeanTwistRotateAxis>();
        if (leanTwistRotateAxis != null)
        {
            leanTwistRotateAxis.enabled = false;
        }
        else
        {
            Debug.LogWarning("LeanTwistRotateAxis component not found on the parent GameObject.");
        }

        parent.GetComponent<LeanPinchScale>().enabled = true;
        parent.GetComponent<LeanTwistRotateAxis>().enabled = true;
    }
}
