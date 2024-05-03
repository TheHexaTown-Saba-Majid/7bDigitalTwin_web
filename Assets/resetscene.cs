using UnityEngine;
using UnityEngine.SceneManagement;

public class resetscene : MonoBehaviour
{  public void ResetScene()
    {
        // Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // Reload the current scene
        SceneManager.LoadScene(currentScene.name);
    }
}

