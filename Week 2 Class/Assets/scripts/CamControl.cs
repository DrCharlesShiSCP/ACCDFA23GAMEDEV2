using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    public float cameraSpeed = 5f; // Adjust this value to control the camera's follow speed

    void Update()
    {
        // Get the current position of the cursor in screen space
        Vector3 cursorPosition = Input.mousePosition;

        // Convert the screen space cursor position to world space
        cursorPosition.z = transform.position.z - Camera.main.transform.position.z;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(cursorPosition);

        // Move the camera toward the cursor's position
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }
}
