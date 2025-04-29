using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnExit : MonoBehaviour
{
    public GameObject ExitPnl;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Lock cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            ExitPnl.SetActive(true);
            GameObject.FindGameObjectWithTag("switch").GetComponent<SC_FPSController>().enabled = false;
        }

    }
}
