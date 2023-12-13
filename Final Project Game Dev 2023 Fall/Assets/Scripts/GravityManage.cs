using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GravityManage : MonoBehaviour
{
    Vector3 gravityUp = new Vector3(0, 1, 0);
    Vector3 gravityDown = new Vector3(0, -1, 0);
    Vector3 gravityLeft = new Vector3(-1, 0, 0);
    Vector3 gravityRight = new Vector3(1, 0, 0);
    public float rotationSpeed = 50f;
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Transform playerTransform;
    private Vector3 constantForwardDirection = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Disable default gravity
        rb.useGravity = false;
        playerTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        HandleGravityInput();
    }
    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate forward and right vectors relative to the gravity direction
        Vector3 gravityDirection = -Physics.gravity.normalized;
        Vector3 forward = Vector3.Cross(transform.right, gravityDirection).normalized;
        Vector3 right = Vector3.Cross(gravityDirection, transform.forward).normalized;

        // Adjust movement direction based on the current orientation
        Vector3 movement = (forward * moveVertical + right * moveHorizontal).normalized;

        // Apply movement
        rb.MovePosition(transform.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    void HandleGravityInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            FlipGravity(gravityUp);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            FlipGravity(gravityDown);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FlipGravity(gravityRight);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            FlipGravity(gravityLeft);
        }
    }
    void FixedUpdate()
    {
        // Apply custom gravity
        rb.AddForce(Physics.gravity, ForceMode.Acceleration);
        MaintainUprightOrientation();
        HandleMovement();
    }
    void MaintainUprightOrientation()
    {
        // Adjust the 'up' vector of the player to always face away from the gravity direction
        Vector3 gravityDirection = Physics.gravity.normalized;
        Quaternion targetRotation = Quaternion.FromToRotation(-playerTransform.up, gravityDirection) * playerTransform.rotation;

        // Smoothly rotate the player to maintain upright orientation
        playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }
    void FlipGravity(Vector3 newGravityDirection)
    {
        Physics.gravity = newGravityDirection * 9.81f; // Adjust the multiplier to control gravity strength
        RotatePlayer(newGravityDirection);
    }
    void RotatePlayer(Vector3 newUp)
    {
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, newUp) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bound"))
        {
            ReloadCurrentScene();
        }
        else if (collision.gameObject.CompareTag("pass"))
        {
            LoadHazardsScene();
        }
        else if (collision.gameObject.CompareTag("passhaz"))
        {
            LoadMovingPlatScene();
        }
        else if (collision.gameObject.CompareTag("platpass"))
        {
            LoadChallenge();
        }
        else if (collision.gameObject.CompareTag("hazards"))
        {
            ReloadCurrentScene();
        }
        else if (collision.gameObject.CompareTag("challenge1pass"))
        {
            LoadDarkness();
        }
        else if (collision.gameObject.CompareTag("trap"))
        {
            FlipGravity(gravityUp);
        }
        else if (collision.gameObject.CompareTag("darkpass"))
        {
            LoadChallenge2();
        }
        else if (collision.gameObject.CompareTag("challenge2pass"))
        {
            LoadChallenge3();
        }
        else if (collision.gameObject.CompareTag("challenge3pass"))
        {
            LoadDrop();
        }
        else if (collision.gameObject.CompareTag("droppass"))
        {
            LoadChallenge4();
        }
        else if (collision.gameObject.CompareTag("challenge4"))
        {
            LoadMenu();
        }
        void LoadChallenge4()
        {
            SceneManager.LoadScene("Challenge4");
            FlipGravity(gravityDown);
        }
        void ReloadCurrentScene()
        {
            // Get the current scene name and reload it
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
            FlipGravity(gravityDown);
        }

        void LoadHazardsScene()
        {
            SceneManager.LoadScene("Hazards");
            FlipGravity(gravityDown);
        }
        void LoadDrop()
        {
            SceneManager.LoadScene("Drop");
            FlipGravity(gravityDown);
        }
        void LoadChallenge2()
        {
            SceneManager.LoadScene("Challenge2");
            FlipGravity(gravityDown);
        }
        void LoadChallenge3()
        {
            SceneManager.LoadScene("Challenge3");
            FlipGravity(gravityDown);
        }
        void LoadMovingPlatScene()
        {
            SceneManager.LoadScene("Moving Platforms");
            FlipGravity(gravityDown);
        }
        void LoadChallenge()
        {
            SceneManager.LoadScene("Challenge1");
            FlipGravity(gravityDown);
        }
        void LoadDarkness()
        {
            SceneManager.LoadScene("Darkness");
            FlipGravity(gravityDown);
        }
        void LoadMenu()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
