using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the character movement

    // Update is called once per frame
    void Update()
    {
        // Get the input from the horizontal and vertical axes (configured in the Input Manager)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the direction of movement based on the input and the speed
        Vector3 movement = new Vector3(horizontal, 0.0f, vertical) * speed * Time.deltaTime;

        // Move the game object to the new position
        transform.Translate(movement, Space.World);

        // Optionally, if you want the game object to face the direction it's moving,
        // uncomment the following line of code:
        // if (movement != Vector3.zero) transform.forward = movement;
    }

}
