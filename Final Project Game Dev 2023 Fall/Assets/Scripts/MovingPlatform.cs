using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 3.0f; // Speed of the platform's movement
    public float maxDistance = 5.0f; // Maximum distance the platform moves from its starting position

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Store the starting position of the platform
    }

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        // Calculate the new position
        Vector3 newPosition = startPosition;
        newPosition.z = startPosition.z + Mathf.Sin(Time.time * speed) * maxDistance;

        // Apply the new position
        transform.position = newPosition;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
