using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMesh : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent; // Reference to the NavMeshAgent component
    private bool isMoving; // Flag to track if the player is currently moving
    public CharacterSelectionAndMove csw;
    public PlayerManager PM;
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
    //½âËø½ÇÉ«
    public void UnLock()
    {
        csw.PlayerMoveBox = false;
        agent.enabled = true;
        this.transform.SetParent(csw.PlayerParent);
        PM.BM.charList.Remove(this.transform.GetComponent<PlayerNavMesh>());
    }
    void HandlePlayerClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)&&csw.gotSelect)
        {
            CharacterSelectionAndMove temp = this.transform.GetComponent<CharacterSelectionAndMove>();
            // Check if the hit point is on the NavMesh
            if (hit.collider.CompareTag("Ground")&& !temp.PlayerMoveBox)
            {
                // Set the destination for the NavMeshAgent
                agent.SetDestination(hit.point);
                isMoving = true;
            }
            //¼ì²âÊÇ·ñµã»÷±¦Ïä
            if (hit.collider.CompareTag("Box") && !temp.PlayerMoveBox)
            {
                if (Vector3.Distance(this.transform.position, PM.Box.position)<8)
                {
                    temp.gotSelect = false;
                    temp.PlayerMoveBox = true;
                    temp.agent.isStopped = true;
                    temp.agent.velocity = Vector3.zero;
                    temp.agent.enabled = false;
                    this.transform.SetParent(PM.SetChar());
                    this.transform.localPosition = new Vector3(0, 0, 0);
                    PM.BM.charList.Add(this);
                }
            }
        }
    }

    // Check if the player has reached their destination
    void LateUpdate()
    {
        if (isMoving && agent != null&&!csw.PlayerMoveBox)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                isMoving = false;
                // You can add custom logic here when the player reaches the destination
            }
        }
    }
}
