using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(PolygonCollider2D))]
public abstract class Pickable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{/*
    [Header("++Ray Settings++")]
    [SerializeField] protected Transform rayPoint;*/

    [Header("++PickingOffset++")]
    [SerializeField] Vector3 offset;

    [Header("Animation")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected float moveDistanceY = 0.01f;
    bool _toolUp = false;

    // different states
    public enum State { Free = 0, Busy = 1, Dragging = 2, NonDragable = 3 }
    [field: SerializeField] public State CurrentState { protected set; get; }

    [SerializeField] protected List<Transform> targetObjects = new List<Transform>();
    protected int objectiveCount;

    protected RaycastHit hitInfo { private set; get; }

    // private vars
    private Vector3 orgPosition;
    private Transform orgTransform;
    public Camera mainCamera;
    private Sequence sequence;

    protected void Start()
    {
        // store original position
        orgPosition = transform.position;
        orgTransform = this.transform;

        // get main camera from GameManaegr
        mainCamera = GameManager.instance.MainCamera;

        // calculate offset
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // offset.z = transform.position.z - mainCamera.transform.position.z;
    }

    public void RecalculateOffsetZ() => offset.z = transform.position.z - mainCamera.transform.position.z;
    protected void RenewOffsetZ(float newZ) => offset.z = newZ;

    public void UpDownAnimation()
    {
        print("UpDownAnimation " + name);
        // if being dragged by user then don't play animation
        if (CurrentState.Equals(State.Dragging))
        {

            // BackInPosition();
            return;
        }

        // animate if not already Animating
        if (sequence != null)
            return;

        // and if on Original position, only then animate
        if (!(transform.position == orgPosition))
            return;
        /*if (CurrentState.Equals(State.Free))
        {*/
        /*    if(GameManager.instance.CurrentCareType == CareType.NoseCare) 
            {
                moveDistanceY = 0.005f;
            }   
            else
                moveDistanceY = 0.01f;*/
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(transform.position.y + moveDistanceY, 0.5f));

        // Set the loop options
        sequence.SetLoops(-1, LoopType.Yoyo);

        /*  }
          else print("Not animating up down tool is not free");*/

    }

    public void AutoUpdateOrgPosition()
    {
        print("AutoUpdateOrgPosition" + name);
        orgPosition = transform.position;
    }

  /*  public void OnPointerDown(PointerEventData eventData)
    {
        print("----OnPointerDown" + CurrentState);
        if (CurrentState.Equals(State.Free))
        {
            _toolUp = true;
            //print("---_toolUp " + _toolUp);


            OnDrag(eventData);
        }
        else
        {
            _toolUp = false;
            //print(name + " CurrentState " + CurrentState);
            return;
        }

        // Busy up Other tools
        //GameManager.BusyUpTools?.Invoke();

        // stop up down animation
        if (sequence != null)
            if (sequence.IsPlaying())
            {
                // Debug.Log("Stoppping UpDown Animation " + name);

                sequence.Kill();
                sequence = null;
            }

        
    if (HelperHand.Instance.InMotion)
         {
             // if hand is animating stop it's motion
             HelperHand.Instance.Stop();
         }
    }
*/
    /* public void OnDrag(PointerEventData eventData)
     { 
         if (CurrentState.Equals(State.Busy) ||
             CurrentState.Equals(State.NonDragable))
         { return; }

         else
         {
              if (_toolUp)
             { 
                  print(transform.position  + "position---   : CurrentState " + CurrentState);
                 CurrentState = State.Dragging;

                 Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                 transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, 0);
                 Ray ray = new Ray(rayPoint.position, Vector3.forward);

                 Debug.DrawRay(ray.origin, ray.direction, Color.red);

                 if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 1f))
                     hitInfo = hit;
                 else hitInfo = default;
                 //PlaySound();
             }
         }
     }
    */
   /* public void OnPointerExit(PointerEventData eventData)
    {
        BackInPosition();
        print("pointer exit");
    }*/
    [SerializeField] Transform image;
    public void OnBeginDrag(PointerEventData eventData)
    {
        //iTween.ScaleTo(image.gameObject, iTween.Hash("scale", 1.2f, "easetype", iTween.EaseType.easeOutBounce));
        print("----OnPointerDown" + CurrentState);
        if (CurrentState.Equals(State.Free))
        {
            _toolUp = true;
            //print("---_toolUp " + _toolUp);


            OnDrag(eventData);
        }
        else
        {
            _toolUp = false;
            //print(name + " CurrentState " + CurrentState);
            return;
        }

        image.localScale = Vector3.one * 2;
        print("in drag begin");
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (CurrentState.Equals(State.Busy) ||
            CurrentState.Equals(State.NonDragable))
        { return; }

        else
        {
            if (_toolUp)
            {
                CurrentState = State.Dragging;
                 // transform.position = new Vector3(image.position.x + eventData.delta.x, image.position.y + eventData.delta.y, 0); 
                    transform.position = mainCamera.ScreenToWorldPoint( new Vector3( eventData.position.x, eventData.position.y , 0) + offset); //for chalk
              
                /*Ray ray = new Ray(rayPoint.position, Vector3.forward);

                Debug.DrawRay(ray.origin, ray.direction, Color.red);

                if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 1f))
                    hitInfo = hit;
                else hitInfo = default;*/
                // print("hitInfo ..." + hitInfo);


                // Create a 2D ray from the mouse position
                /* Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                 Ray2D _ray = new Ray2D(mousePosition, Vector2.zero);

                 // Perform the raycast
                 RaycastHit2D _hit = Physics2D.Raycast(_ray.origin, _ray.direction);
                 Debug.DrawRay(transform.position, Vector3.forward, Color.red);

                 // Check if the ray hits something
                 if (_hit.collider != null)
                 {
                     // Do something with the hit information
                     Debug.Log("Hit object: " + _hit.collider.gameObject.name);
                 }*/

            }
        }
       // CheckAndRemoveTarget(targetObject);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.localScale = Vector3.one;
        if (CurrentState.Equals(State.Busy))
            return;
        if (CurrentState.Equals(State.Dragging))
        {
            CurrentState = State.Free;
            //Debug.Log("setting free " + name);
            BackInPosition();
            // Free up Other tools
            //GameManager.FreeUpTools?.Invoke();
        }
        print("pointer exit");
    }
    public abstract void PlaySound();


    /*public void OnPointerEnter(PointerEventData eventData) => Debug.Log("Over :" + name);*/

  /*  public void OnPointerUp(PointerEventData eventData)
    {
        print("OnPointerUp---------- CurrentState " + CurrentState);
        // if object was being dragged then return to original position, the object can be in busy state here if it starts an Animation
        if (CurrentState.Equals(State.Busy))
            return;
        if (CurrentState.Equals(State.Dragging))
        {
            CurrentState = State.Free;
            //Debug.Log("setting free " + name);
            BackInPosition();
            // Free up Other tools
            //GameManager.FreeUpTools?.Invoke();
        }

        *//* // If any object is playing sound stop it
         if (SFXManager.instance.IsPlaying)
         {
             SFXManager.instance.Stop();
         }*//*
    }
*/
    /// <summary>
    /// makes the object busy, so it can't be dragged over screen
    /// </summary>
    public void MakeBusy() => CurrentState = State.Busy;

    /// <summary>
    /// makes the object free, so it can be dragged over screen
    /// </summary>
    public void ClearState()
    {
        print("clear state " + name);
        StartCoroutine(ClearStateRoutine());
    } 
    IEnumerator ClearStateRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        BackInPosition();
        CurrentState = State.Free;
        print(name + " " + CurrentState);
    }

    public void MarkNonDragable()
    {
        CurrentState = State.NonDragable;
    }

    // Animation Functions
    private void Animating()
    {
        /*-----This function is called by animation event , in animator--------*/
        print("Animating" + name);
        CurrentState = State.Busy;
        hitInfo = default;
    }
    private void AnimationComplete()
    {
        /*-----This function is called by animation event , in animator--------*/
        // if current was not marked as NonDragable, and was Busy then change to Free, 
        if (CurrentState == State.Busy)
            MarkNonDragable();
        // ClearState();
        // MarkNonDragable();
        print("AnimationComplete");

        BackInPosition();
        // if was animating then freeup Other tools
        //GameManager.FreeUpTools?.Invoke();
        if (sequence != null)
            if (sequence.IsPlaying())
            {
                // Debug.Log("Stoppping UpDown Animation " + name);

                sequence.Kill();
                sequence = null;
            }
        /* if (SFXManager.instance.IsPlaying)
             SFXManager.instance.Stop();*/
    }

    /// <summary>
    /// animates the object back into its previous/Original Position
    /// </summary>
    protected void BackInPosition()
    {
        if (!transform.position.Equals(orgPosition))
        {
            Debug.Log("BackInPosition  " + name);
            transform.DOMove(orgPosition, 0.5f).SetEase(Ease.Linear); /*.OnComplete(ClearState)*/
        }
    }

    void SetRestrictorOff()
    {
        /*if (ObjectRestrictorToSelectNextObject.instance)
            ObjectRestrictorToSelectNextObject.instance.ColliderStatus();*/
    }

    // helper functions
    protected void AutoAdjustPosition()
    {
        print("autoadjustposition");
        Vector3 position = hitInfo.transform.position;
        Vector3 newPositon = new Vector3(position.x, position.y, transform.position.z);

        transform.DOMove(newPositon, 0.3f).OnComplete(() =>
        {
            Debug.Log("Reached Final Position");
        });
    }

    protected bool CheckTarget(int instanceID) => targetObjects.Any(x => x.GetInstanceID().Equals(instanceID));

    protected bool CheckAllTargets(int instanceID, out Transform targetObject)
    {
        Transform target = targetObjects.FirstOrDefault(x => x.GetInstanceID().Equals(instanceID));

        targetObject = target;

        if (target != null)
        {
            return true;
        }
        else return false;
    }
    protected bool CheckAndRemoveAllTargets(int instanceID, out Transform targetObject)
    {
        Transform target = targetObjects.FirstOrDefault(x => x.GetInstanceID().Equals(instanceID));

        targetObject = target;

        if (target != null)
        {
            targetObjects.Remove(target);

            return true;
        }
        else return false;
    }

    protected bool CheckAndRemoveTarget(int instanceID, out Transform targetObject)
    {
        Debug.Log("Getting Target", gameObject);
        Transform target = targetObjects.FirstOrDefault(x => x.GetInstanceID().Equals(instanceID));

        targetObject = target;

        if (target != null)
        {
            CurrentState = State.NonDragable;
            targetObjects.Remove(target);
            return true;
        }
        else return false;
    }

    protected bool CheckFirst(int instanceID, out Transform targetObject)
    {
        if (instanceID == targetObjects[0].transform.GetInstanceID())
        {
            targetObject = targetObjects[0];
            return true;
        }
        targetObject = null;
        return false;
    }

    protected void RemoveTarget(Transform target) => targetObjects.Remove(target);

    protected bool CheckFirstAndRemove(int instanceID, out Transform targetObject)
    {
        if (instanceID == targetObjects[0].transform.GetInstanceID())
        {
            targetObject = targetObjects[0];
            targetObjects.Remove(targetObject);
            return true;
        }
        targetObject = null;
        return false;
    }

    public void AddTarget(Transform target) => targetObjects.Add(target);

    // some Animation Events, called from tools


}