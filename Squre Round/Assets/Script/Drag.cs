using UnityEngine;

public class Drag : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isDragging = false;

    void Update()
    {
        // While the mouse button is down, update the position of the GameObject
        if (isDragging)
        {
            // Current mouse position in screen space (x, y, screenPoint.z)
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            // Convert to world space
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;

            // Set position of GameObject
            transform.position = currentPosition;
        }
    }

    void OnMouseDown()
    {
        // When the mouse button is pressed, record the offset from the mouse to the object position
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        // Calculate offset between object position and mouse position
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

        // Begin dragging
        isDragging = true;
    }

    void OnMouseUp()
    {
        // When the mouse button is released, stop dragging
        isDragging = false;
    }
}
