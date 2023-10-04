using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public CharacterSelectionAndMove click;


    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            HandleCharacterSelection();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            //HandleCharacterMove();
        }

        // If the character is selected and the destination is set, move it
    }
    // Start is called before the first frame update
    void HandleCharacterSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject clickedObject = hit.collider.gameObject;
            // Check if the clicked object is a selectable character
            if (clickedObject.CompareTag("Characters") && !clickedObject.GetComponent<CharacterSelectionAndMove>().gotSelect)
            {
                // Deselect the currently selected character (if any)
                //clickedObject.GetComponent<CharacterSelectionAndMove>().DeselectCharacter();

                // Select the clicked character
                //clickedObject.GetComponent<CharacterSelectionAndMove>().SelectCharacter();
                clickedObject.GetComponent<CharacterSelectionAndMove>().gotSelect = true;
            }else if (clickedObject.CompareTag("Characters") && clickedObject.GetComponent<CharacterSelectionAndMove>().gotSelect)
            {
                clickedObject.GetComponent<CharacterSelectionAndMove>().gotSelect = false;
            }
        }
    }
}
