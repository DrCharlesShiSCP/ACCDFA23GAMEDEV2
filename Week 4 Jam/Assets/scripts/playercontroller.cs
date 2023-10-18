using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject player;
    public float moveAmount = 1.0f;
    public float rollSpeed = 5f;
    public Rigidbody rb;
    public int maxJumps = 3;      // Maximum number of consecutive jumps
    private int remainingJumps;   // Number of remaining jumps
    public TextMeshProUGUI jumpText;

    private bool isGrounded;      // Flag to check if the character is grounded

    void Start()
    {
        // Get the Rigidbody component if it's not assigned in the Inspector
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        remainingJumps = maxJumps;
        UpdateJumpText(); // Update the jump text on start
    }
    void Update()
    {
        Vector3 rollDirection = Vector3.left;
        // Calculate the new position in the -Z direction based on rollSpeed
        Vector3 moveVector = Vector3.left * rollSpeed * Time.deltaTime;

        // Apply the movement to the player's position
        transform.Translate(moveVector);

        if (Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0)
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        // Check if the character is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }
    void Jump()
    {
        // Apply the jump force in the upward direction (world space)
        rb.AddForce(Vector3.up * moveAmount, ForceMode.Impulse);
        // Reduce the remaining jumps
        remainingJumps--;
        // Clamp the remaining jumps to a minimum of 0
        remainingJumps = Mathf.Max(remainingJumps, 0);
        UpdateJumpText(); // Update the jump text after each jump
    }
    public void ResetJumpCount()
    {
        remainingJumps = maxJumps;
    }

    // OnCollisionEnter is called when the character touches a collider
    void OnCollisionEnter(Collision collision)
    {
        // Check if the character collides with game objects tagged as "floor"
        if (collision.gameObject.CompareTag("floor"))
        {
            // Reset the jump count
            ResetJumpCount();
        }
    }
    void UpdateJumpText()
    {
        if (jumpText != null)
        {
            jumpText.text = "Jumps: " + remainingJumps.ToString();
        }
    }

}
