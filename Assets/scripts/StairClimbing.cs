using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class StairClimbing : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float stepHeight = 0.5f;  // Height of each step
    public float maxStepDistance = 1.0f;  // Maximum distance for step detection

    public GameObject kneeObject;
    public GameObject footObject; 


    void Update()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        // Cast rays from knee and foot positions
        RaycastHit kneeHit;
        RaycastHit footHit;

        if (Physics.Raycast(kneeObject.transform.position, Vector3.down, out kneeHit, maxStepDistance))
        {
            // Knee ray hit something
            if (Physics.Raycast(footObject.transform.position, Vector3.down, out footHit, maxStepDistance))
            {
                // Foot ray hit something
                float kneeToFootDistance = Vector3.Distance(kneeHit.point, footHit.point);

                if (kneeToFootDistance > stepHeight)
                {
                    Debug.Log("step!");
                    transform.Translate(Vector3.up * stepHeight);
                    Debug.Log("step!");
                }
                else
                {
                    // Hurdle detected
                    Debug.Log("Hurdle detected!");
                }
            }
        }

        // Player movement controls (left, right, forward, backward)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}