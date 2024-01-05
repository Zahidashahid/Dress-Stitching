// ClothPiece.cs
using UnityEngine;
using UnityEngine.XR;

public class ClothPiece : Pickable
{
    private bool isDragging = false;
    public int clothPieceNum;
    //  private Vector3 offset;
    public bool setToDropZone = false;
    private void OnMouseDown()
    {
        /*offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;*/
    }
   
    private void OnMouseUp()
    {
        isDragging = false;
    }
    
        private void Update()
        { 
            if (CurrentState.Equals(State.Dragging) )
            {
                if (hitInfo.transform != null)
                {
                     print(hitInfo.collider.tag);
                    CheckRespectiveSlotForCutPiece();
                }
            }
        }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DropZone dropZone = collision.GetComponent<DropZone>();
        if (dropZone.acceptedclothPieceNum == clothPieceNum)
        {
            print("Collider 2d function  " +
                collision.tag);
            transform.position = collision.transform.position;
            MarkNonDragable();

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        print("Collider 3d function");
    }
    private void CheckRespectiveSlotForCutPiece()
    {
        // if Instance ID matches then
        print("check for slot");

        /*if (CheckAndRemoveTarget(hitInfo.transform.GetInstanceID(), out Transform target))
        {
            print("check for slot");
            DropZone dropZone = target.GetComponent<DropZone>();

             
            AutoUpdateOrgPosition();
            MarkNonDragable();
        }*/
    }
    public override void PlaySound()
    {
    }
}
