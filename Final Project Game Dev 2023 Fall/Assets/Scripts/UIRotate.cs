using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRotate : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Image axisImage; // Reference to the UI element

    void Update()
    {
        UpdateAxisImage();
    }

    void UpdateAxisImage()
    {
        if (player != null && axisImage != null)
        {
            // Get the inverse of the player's Z-axis rotation
            float inverseZRotation = -player.eulerAngles.z;

            // Apply this rotation to the UI element
            axisImage.rectTransform.rotation = Quaternion.Euler(0, 0, inverseZRotation);
        }
    }
}
