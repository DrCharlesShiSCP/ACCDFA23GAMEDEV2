using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    public Transform player; // Assign your player object here
    public Vector3 offset; // Set this to position the camera relative to the player
    public float smoothSpeed = 0.125f; // Adjust for smoother or more rigid camera movement
    public bool followRotation = true; // Set to true if you want the camera to rotate with the player

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + player.TransformDirection(offset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        if (followRotation)
        {
            // Align the camera's up vector with the player's up vector
            Vector3 relativeUp = player.up;
            Vector3 relativeForward = player.TransformDirection(Vector3.forward); // Alternatively, use a different vector if you want to adjust the camera's forward direction

            Quaternion targetRotation = Quaternion.LookRotation(relativeForward, relativeUp);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
        }
    }
}
