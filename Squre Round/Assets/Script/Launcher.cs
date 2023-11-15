using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject prefabToLaunch; // Assign this in the inspector with your prefab
    public float launchForce = 10.0f; // The force at which the prefab will be launched

    // Update is called once per frame
    void Update()
    {
        // Check if the space key was pressed this frame
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchPrefab();
        }
    }

    void LaunchPrefab()
    {
        if (prefabToLaunch != null)
        {
            // Instantiate the prefab at the current position and rotation of the launcher
            GameObject launchedObject = Instantiate(prefabToLaunch, transform.position, transform.rotation);

            // Get the Rigidbody component and add force to it
            Rigidbody rb = launchedObject.GetComponent<Rigidbody>();
            if (rb != null) // Make sure the Rigidbody component exists to avoid errors
            {
                // Calculate the direction based on the launcher's forward direction
                Vector3 dir = transform.forward; // This is the forward direction the launcher is facing

                // Add force in the direction the launcher is facing
                rb.AddForce(dir * launchForce, ForceMode.VelocityChange);
            }
            else // Log an error message if there's no Rigidbody
            {
                Debug.LogError("The prefab needs a Rigidbody component to be launched.");
            }
        }
        else // Log an error message if no prefab is assigned
        {
            Debug.LogError("No prefab assigned to be launched. Please assign a prefab to the Launcher script.");
        }
    }
}

