using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    public GameObject loadingScreen; // Reference to the loading screen GameObject

    public void Load(string scenename)
    {
        PlayerPrefs.DeleteAll();
        StartCoroutine(LoadSceneAsync(scenename));
    }

    IEnumerator LoadSceneAsync(string scenename)
    {
        // Display the loading screen if it exists
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }

        // Start loading the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenename);

        // Wait until the asynchronous scene loading is complete
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Hide the loading screen when done
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }
    }
}
