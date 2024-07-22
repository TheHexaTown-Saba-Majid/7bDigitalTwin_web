using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public accordianDropDown dropdown1;
    public accordianDropDown dropdown2;
    private void Update()
    {
        if (dropdown1 != null)
        {
            if (dropdown1.isOpen)
            {
                dropdown1.animator.Play("testing");
            }
            else
            {
                dropdown1.animator.Play("reverse");
            }
        }
    }
}
