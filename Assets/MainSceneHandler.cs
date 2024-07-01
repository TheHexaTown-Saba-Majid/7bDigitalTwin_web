using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneHandler : MonoBehaviour
{

    public Animator startAnim; // Reference to the Animator component
    public string animationName; // The name of the animation to check

    void Update()
    {
        // Get the current state info of the Animator for layer 0
        AnimatorStateInfo stateInfo = startAnim.GetCurrentAnimatorStateInfo(0);

        // Check if the current animation is not playing or has finished
        if (stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1f)
        {
            Destroy(startAnim); // Destroy the Animator component
            return;

        }
    }
}
