using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    public void LoadA(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
