using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform target; // The target object to rotate around
    public float yOffset = 20.0f; // Distance from the target object on the y-axis

    private Vector3 offset; // Offset vector between camera and target

    void Start()
    {
        if (target != null)
        {
            // Set the initial offset. This uses the camera's current x and z but takes the y from the target plus yOffset.
            offset = new Vector3(0, target.position.y + yOffset, 0);
        }
        else
        {
            Debug.LogError("Target not set for CameraRotate. Please assign a target Transform in the Inspector.");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Update the camera's position to the target's position plus the offset
            // This keeps the camera at the same x and z position relative to the target but uses the yOffset for the y position.
            transform.position = new Vector3(target.position.x, target.position.y + yOffset, target.position.z);

            // Rotate the camera to always look at the target
            transform.LookAt(target);

            // Apply a fixed 90 degree rotation on the camera's x-axis to tilt down or up.
            transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }
    }
}
