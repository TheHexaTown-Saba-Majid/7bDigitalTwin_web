using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CategoryManager : MonoBehaviour
{
    public void DiningChair()
    {
        PlayerPrefs.SetString("TargetObjectName", "Scroll-Snap-DiningSet");
        SceneManager.LoadScene("AR_Furniture");
    }
    public void Lamps()
    {
        PlayerPrefs.SetString("TargetObjectName", "Scroll-Snap-Lamps");
        SceneManager.LoadScene("AR_Furniture");
    }
    public void Plants()
    {
        PlayerPrefs.SetString("TargetObjectName", "Scroll-Snap-Plants");
        SceneManager.LoadScene("AR_Furniture");
    }
    public void Sofa()
    {
        PlayerPrefs.SetString("TargetObjectName", "Scroll-Snap-Sofa");
        SceneManager.LoadScene("AR_Furniture");
    }
}
