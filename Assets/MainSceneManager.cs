using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{   
    public Animator animator;
    public string animationStateName;
    private Coroutine currentCoroutine;
    public Camera[] cameras;

    void Start()
    {
        if (animator != null)
        {
            animator.enabled = false;
        }
    }
    public void ControlAnimator()
    {
        Model();

        if (animator != null)
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(EnableAnimatorTemporarily());
        }
        
    }
    private IEnumerator EnableAnimatorTemporarily()
    {
        animator.enabled = true;
        animator.Rebind();
        animator.Update(0);
        animator.Play(animationStateName);
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        float animationLength = stateInfo.length;
        yield return new WaitForSeconds(animationLength);
        animator.enabled = false;
        currentCoroutine = null;

    }
    public void BirdView()
    {
        for (int i = 0; i < cameras.Length- 1; i++) {

            cameras[i].gameObject.SetActive(false);
        }
        cameras[cameras.Length - 1].gameObject.SetActive(true);
    }
    public void Model()
    {
        for (int i = 1; i < cameras.Length; i++)
        {

            cameras[i].gameObject.SetActive(false);
        }
        cameras[0].gameObject.SetActive(true);
    }
    public void help()
    {
        Application.OpenURL("https://www.thehexatown.com/");
    }
}
