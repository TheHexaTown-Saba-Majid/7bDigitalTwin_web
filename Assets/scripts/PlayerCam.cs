using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    public float sesnX;
    public float sesnY;

    public Transform Orientation;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    // Update is called once per frame
    void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sesnX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sesnY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation= Mathf.Clamp(xRotation, -90f, 90f);

        //Rotate Camera and Orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        Orientation.rotation = Quaternion.Euler(xRotation, yRotation, 0);

    }
}
