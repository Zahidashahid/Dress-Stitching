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
    new void Start()
    {
        base.Start();
       // canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

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
    void ContinueDrawing(Vector2 linestartPosition)
    {
        Debug.Log("Drawing line ");
           Vector3 mousePosition = linestartPosition;
         Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
         worldPosition.z = 0f; // Set the distance to the camera
         int currentPosition = lineRenderer.positionCount;
         lineRenderer.positionCount = currentPosition + 1;
         lineRenderer.SetPosition(currentPosition, worldPosition); 

          
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
