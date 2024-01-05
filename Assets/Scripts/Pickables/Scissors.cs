using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors : MonoBehaviour
{
    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }

        if (isDragging)
        {
            DragScissors();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }
    }

    void StartDragging()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Scissors"))
        {
            isDragging = true;
        }
    }

    void DragScissors()
    {
        if (isDragging)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f; // Set the depth to ensure the scissors are visible

            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }
    }

    void StopDragging()
    {
        isDragging = false;
    }
}
