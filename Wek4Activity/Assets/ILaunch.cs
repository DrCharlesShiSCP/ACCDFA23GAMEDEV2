using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILaunch : MonoBehaviour
{
    public GameObject[] shapes; // Array of shapes to be launched.

    private List<GameObject> availableShapes; // List to store available shapes for launching.

    private void Start()
    {
        // Initialize the availableShapes list with the shapes array.
        availableShapes = new List<GameObject>(shapes);
    }

    private void Update()
    {
        // Check for a mouse click.
        if (Input.GetMouseButtonDown(0))
        {
            // Check if there are available shapes to launch.
            if (availableShapes.Count > 0)
            {
                // Randomly select a shape from the availableShapes list.
                int randomIndex = Random.Range(0, availableShapes.Count);
                GameObject shapeToLaunch = availableShapes[randomIndex];

                // Launch the selected shape.
                LaunchShape(shapeToLaunch);

                // Remove the launched shape from the availableShapes list.
                availableShapes.RemoveAt(randomIndex);

                // Check if all shapes have been launched.
                if (availableShapes.Count == 0)
                {
                    Debug.Log("All shapes have been launched. Refilling the list.");
                    availableShapes.AddRange(shapes); // Refill the list with all shapes.
                }
            }
            else
            {
                Debug.Log("All shapes have been launched.");
            }
        }
    }

    private void LaunchShape(GameObject shape)
    {
        // Instantiate the selected shape and position it at the launcher's position.
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(shape, spawnPosition, spawnRotation);
    }
}
