using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetHint : MonoBehaviour
{
    public GameObject objectToDisable;
    private void Start()
    {
        if (PlayerPrefs.GetInt("DisableObjectOnReload", 0) == 1)
        {
            objectToDisable.SetActive(false);
        }
        else
        {
         
            objectToDisable.SetActive(true);
        }
    }
    public void ReloadSceneAndDisable()
    {
        PlayerPrefs.SetInt("DisableObjectOnReload", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}