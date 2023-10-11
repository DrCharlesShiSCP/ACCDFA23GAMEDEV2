using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform component.
    public Vector3 offset; // Offset between the camera and the player.

    void LateUpdate()
    {
        // Set the camera's position to the player's position plus the offset.
        transform.position = player.position + offset;
    }
}
