using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{
  public  GameObject[] indicators;

    public void OnMouseDown()
    {
        GameObject parentObject = transform.parent.gameObject;
        Animator parentAnimator = parentObject.GetComponent<Animator>();

        if (parentAnimator != null)
        {
          parentAnimator.SetTrigger("HexaBreakDown");

            foreach (GameObject indicator in indicators)
            {

                indicator.SetActive(true);
            }
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Parent object does not have an Animator component.");
        }
    }
}
