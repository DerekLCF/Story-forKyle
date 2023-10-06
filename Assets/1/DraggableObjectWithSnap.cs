using UnityEngine;

public class DraggableObjectWithSnap : MonoBehaviour
{
    public bool isDragging = false;
    private Vector3 offset;
    public bool isSnapped = false;
    public Transform snapTarget;

    private void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - GetMouseWorldPosition();
        isSnapped = false; // Reset the snapped state when dragging starts
    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (isSnapped)
        {
            // If the object was snapped, detach it from the snap target
            transform.SetParent(null);
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.name!= "Card")
        {
            snapTarget = other.transform;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        snapTarget = null;
    }

    private void Update()
    {
        if (isDragging != true && snapTarget != null && !isSnapped)
        {
            transform.position = snapTarget.position;
            isSnapped = true;
        }
        else { 
        
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}