using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chalk : Pickable, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public LineRenderer lineRenderer;  
    private bool isDrawing = false; 
    [SerializeField] private Transform chalkTipPos;
    [SerializeField]  private RectTransform canvasRectTransform;
   /// <summary>
   /// //
   /// </summary>
    public Material lineMaterial;
    public float lineWidth = 0.1f;
    new void Start()
    {
        base.Start(); 
         
    }
    public new void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        StartDrawing();
    } 
    public new void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        ContinueDrawing(chalkTipPos.position);
    } 
    public new void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        StopDrawing();
    } 
    void StartDrawing()
    {
        isDrawing = true;
        lineRenderer.positionCount = 0;
    } 
     [Header("++Ray Settings++")]
    [SerializeField] protected Transform rayPoint;
    void ContinueDrawing(Vector2 linestartPosition)
    {
        Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Perform a 2D raycast from the mouse position
        RaycastHit2D hit = Physics2D.Raycast(_mousePosition, Vector2.zero);
        if (hit.collider != null) 
        {
            Debug.Log("not null  " + hit.collider.name);

            if (hit.collider.tag == "Shape")
            {
                Debug.Log("Drawing line " + hit.collider.name);
               
                    /*-------Draw line on mouse position-----*/
                /* Vector3 mousePosition = Input.mousePosition;
                 //Vector3 mousePosition = linestartPosition;
                 mousePosition.z = Mathf.Abs(mainCamera.transform.position.z); // Set the distance to the camera
                 Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);

                 int currentPosition = lineRenderer.positionCount;
                 lineRenderer.positionCount = currentPosition + 1;
                 lineRenderer.SetPosition(currentPosition, worldPosition);*/
               
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DropZone dropZone = collision.GetComponent<DropZone>();
        Vector3[] linePositions = new Vector3[dropZone.points.Length];
        for (int i = 0; i < dropZone.points.Length; i++)
        {
            linePositions[i] = dropZone.points[i].position;
            linePositions[i].z = 0;
        }

        lineRenderer.positionCount = linePositions.Length;
        lineRenderer.SetPositions(linePositions);
         
            print("Collider chalk " +   collision.tag);
            transform.position = collision.transform.position;  // animation of chalk
          //  MarkNonDragable();
         
    }

    void StopDrawing()
    {
        isDrawing = false;
    }
        public override void PlaySound()
    {
        throw new System.NotImplementedException();
    }
}
