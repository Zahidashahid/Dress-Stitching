using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
public class PathGenerator : MonoBehaviour
{

    [SerializeField] bool rand = false;
    [SerializeField] bool go = true;
    [SerializeField] bool isStopAtEndPoint;

    [SerializeField] int num = 0;
    [SerializeField] float minDist;
    [SerializeField] float AccelerationSpeed;
    [SerializeField] float RotationSpeed;


    [Header("Arrays")]
    [SerializeField] DOTweenAnimation[] tyresAnim;
    [SerializeField] Transform[] Waypoints;



    AudioSource horn;
    Transform thisTransform;
    new Rigidbody rigidbody;
    void Start()
    {
        thisTransform = transform;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.mass = 500f;
        horn = GetComponent<AudioSource>();

    }

    float currentDist;
    void Update()
    {
       // if (UiManager.isGamePaused) return;

        currentDist = Vector3.Distance(thisTransform.position, Waypoints[num].position);

        if (go)
        {
            if (currentDist > minDist)
            {
                Move(num);
            }
            else
            {
                if (!rand)
                {
                    if (!isStopAtEndPoint)
                    {
                        if (num + 1 == Waypoints.Length)
                        {
                            num = 0;
                        }
                        else
                        {
                            num++;
                        }
                    }
                    else
                    {
                        if (num + 1 == Waypoints.Length) go = false;
                        else num++;
                    }
                }
                else
                {
                    num = Random.Range(0, Waypoints.Length);
                }
            }


            if (!isPlay) TyresRotation();
        }
        else
        {
            if (isPlay) TyresRotation();
        }

    }
    
    void Move(int _target)
    {
        //transform.LookAt(Waypoints[num].transform.position);
        var targetRotation = Quaternion.LookRotation(Waypoints[_target].position - thisTransform.position);
        thisTransform.rotation = Quaternion.Slerp(thisTransform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        thisTransform.position += thisTransform.forward * AccelerationSpeed * Time.deltaTime;
    }

    bool isPlay = false;
    void TyresRotation()
    {
        isPlay = !isPlay;

        if (isPlay)
        {
            for (int i = 0; i < tyresAnim.Length; i++)
            {
                tyresAnim[i].DORestart();
            }
        }
        else
        {
            for (int i = 0; i < tyresAnim.Length; i++)
            {
                tyresAnim[i].DOPause();
            }
        }
    }

    bool stoppedTheCar;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            go = false;
            horn.Play();
            stoppedTheCar = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stoppedTheCar = false;
            StartCoroutine(RunCarRoutine());
        }
    }

    IEnumerator RunCarRoutine()
    {
        yield return new WaitForSeconds(2f);
        if (!stoppedTheCar)
            go = true;
    }
}
