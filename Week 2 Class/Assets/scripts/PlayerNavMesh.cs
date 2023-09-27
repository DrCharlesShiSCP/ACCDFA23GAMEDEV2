using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent; // Reference to the NavMeshAgent component
    private bool isMoving; // Flag to track if the player is currently moving
    public CharacterSelectionAndMove csw;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Make sure the NavMeshAgent component is attached to the GameObject
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing!");
            enabled = false; // Disable the script to prevent errors
        }
    }

    void Update()
    {
        // Check for player input to initiate movement
        if (Input.GetMouseButtonDown(0))
        {
            HandlePlayerClick();
        }
    }

    void HandlePlayerClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)&&csw.gotSelect)
        {
            // Check if the hit point is on the NavMesh
            if (hit.collider.CompareTag("Ground"))
            {
                // Set the destination for the NavMeshAgent
                agent.SetDestination(hit.point);
                isMoving = true;
            }
        }
    }

    // Check if the player has reached their destination
    void LateUpdate()
    {
        if (isMoving && agent != null)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                isMoving = false;
                // You can add custom logic here when the player reaches the destination
            }
        }
    }
}
