using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class CharacterSelectionAndMove : MonoBehaviour
{
    public bool gotSelect;
    private NavMeshAgent agent; // Reference to the NavMeshAgent component
    private bool isSelected = false; // Flag to track if the character is currently selected
    private Material originalMaterial; // Store the original material
    public Material selectedMaterial; // Assign this in the inspector to set the selected material

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

    void HandleCharacterMove()
    {
        if (isSelected&&gotSelect)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit point is on the NavMesh
                if (hit.collider.CompareTag("Ground") && agent != null)
                {
                    // Set the destination for the NavMeshAgent
                    agent.destination = hit.point;
                }
            }
        }
    }
}
