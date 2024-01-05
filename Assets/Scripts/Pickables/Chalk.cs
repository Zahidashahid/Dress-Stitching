using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chalk : MonoBehaviour
{ 

    private LineRenderer chalkLineRenderer;

    void Start()
    {
        chalkLineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            DrawChalkLine();
        }
    }

    void DrawChalkLine()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;
            hitPoint.y += 0.1f; // Adjust the height to avoid clipping with the cloth

            chalkLineRenderer.positionCount++;
            chalkLineRenderer.SetPosition(chalkLineRenderer.positionCount - 1, hitPoint);
        }
    }
}
