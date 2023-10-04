using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class CharacterSelectionAndMove : MonoBehaviour
{
    public bool gotSelect;
    public NavMeshAgent agent; // Reference to the NavMeshAgent component
    private bool isSelected = false; // Flag to track if the character is currently selected
    private Material originalMaterial; // Store the original material
    public Material selectedMaterial; // Assign this in the inspector to set the selected material

    public PlayerManager PM;
    public bool PlayerMoveBox = false;
    public Transform PlayerParent;
    void Start()
    {
        gotSelect = false;
        agent = GetComponent<NavMeshAgent>();
        originalMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
       if(gotSelect)
        {
            GetComponent<Renderer>().material = selectedMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = originalMaterial;
        }

        if (gotSelect && PlayerMoveBox)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                gotSelect = true;
                PlayerMoveBox = false;
                agent.enabled = true;
                this.transform.SetParent(PlayerParent);
                PM.BM.charList.Remove(this.transform.GetComponent<PlayerNavMesh>());
            }
        }
    }

    public void SelectCharacter()
    {
        isSelected = true;
        GetComponent<Renderer>().material = selectedMaterial;
    }

    public void DeselectCharacter()
    {
        if (gotSelect)
        {
            gotSelect = false;
            GetComponent<Renderer>().material = originalMaterial;
            if (agent != null)
            {
                agent.ResetPath();
            }
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (PlayerMoveBox)
    //    {
    //        if (this.transform.name == "Fire")
    //        {
    //            if (collision.transform.CompareTag("waterwall")|| collision.transform.CompareTag("electriwall"))
    //            {
    //                PlayerMoveBox = false;
    //                agent.enabled = true;
    //                this.transform.SetParent(PlayerParent);
    //                PM.BM.charList.Remove(this.transform.GetComponent<PlayerNavMesh>());
    //            }
    //        }
    //        if (this.transform.name == "Water")
    //        {
    //            if (collision.transform.CompareTag("firewall") || collision.transform.CompareTag("electriwall"))
    //            {
    //                PlayerMoveBox = false;
    //                agent.enabled = true;
    //                this.transform.SetParent(PlayerParent);
    //                PM.BM.charList.Remove(this.transform.GetComponent<PlayerNavMesh>());
    //            }
    //        }
    //        if (this.transform.name == "Electric")
    //        {
    //            if (collision.transform.CompareTag("waterwall") || collision.transform.CompareTag("firewall"))
    //            {
    //                PlayerMoveBox = false;
    //                agent.enabled = true;
    //                this.transform.SetParent(PlayerParent);
    //                PM.BM.charList.Remove(this.transform.GetComponent<PlayerNavMesh>());
    //            }
    //        }
    //    }
        
        
    //}
    //void HandleCharacterMove()
    //{
    //    if (isSelected&&gotSelect)
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;

    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            // Check if the hit point is on the NavMesh
    //            if (hit.collider.CompareTag("Ground") && agent != null)
    //            {
    //                // Set the destination for the NavMeshAgent
    //                agent.destination = hit.point;
    //            }
    //        }
    //    }
    //}
}
