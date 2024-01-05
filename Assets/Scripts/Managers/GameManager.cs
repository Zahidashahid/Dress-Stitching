using System;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
//using GameAnalyticsSDK;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] Camera mainCamera;
    [SerializeField] Animator cameraAnimator;

    [Tooltip("Game Data Asset File")]
  //  [SerializeField] GameData gameData;

    [Header("++Cares++")]
    [SerializeField] Transform[] cares;

    [SerializeField] GameObject postProcessing;

    [Header("++Sounds++")]
    [SerializeField] AudioSource audioSource;
    [Tooltip("Music Clips")][SerializeField] AudioClip[] clips;

    [Header("Background Colors")]
    [SerializeField] Color[] colors;
    [SerializeField] MeshRenderer bgRenderer;

    public Animator CameraAnimator => cameraAnimator;
    public Camera MainCamera => mainCamera;
   /* public float SfxVol => gameData.sfx;
    public float MusicVol => gameData.music;*/

    //public static Action BusyUpTools;
    //public static Action FreeUpTools;

    public static bool Restarted;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

       // cares[(int)gameData.selectedCare].gameObject.SetActive(true);//lEVELtOoPEN
 
       // MainMenu.Opened = true;
    }

    public void UpdateMaxUnlocked()
    {/*
        int currentCareNo = (int)CurrentCareType;
        print("currentCareNo  "  + currentCareNo);
        if (currentCareNo == 9)
            return;

        gameData.levelUnlocked[currentCareNo+1] = true;
        Debug.Log("___UpdateMaxUnlocked()_" + (currentCareNo + 1));*/

    }

   // public CareType CurrentCareType => gameData.selectedCare;

    private void Start()
    {
      /*  audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.volume = MusicVol;
        audioSource.Play();*/
        print("-------Game start------");
        /*---------TO test back care level ---------*/
       //  gameData.selectedCare = CareType.BackCare;
       // string gaEvent = Restarted ? "LevelRestarted_" + gameData.selectedCare : "LevelStarted_" + gameData.selectedCare;
      //  GameAnalytics.NewDesignEvent(gaEvent);

        // some Random Color at BG
        //bgRenderer.material.color = colors[Random.Range(0, colors.Length)];
       // MainMenu.ReturnedFromMainScene = false;
    }

    public void NextCare()
    {/*
         cares[(int)gameData.selectedCare].gameObject.SetActive(false);//lEVELclose
          gameData.selectedCare++;
         cares[(int)gameData.selectedCare].gameObject.SetActive(true);//lEVELtOoPEN*/

    }

    // Update is called once per frame
    void Update()
    {
       /* if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            // Reload Scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }*/

        //if (Keyboard.current.escapeKey.wasPressedThisFrame)
        //{
        //    EditorApplication.isPaused = true;
        //}
    }

    public Vector3 ScreenCenterWorldPoint()
    {
        // Calculate the center screen point in viewport coordinates
        Vector3 centerViewportPoint = new Vector3(0.5f, 0.5f, 0f);

        // Convert viewport point to world space
        return mainCamera.ViewportToWorldPoint(centerViewportPoint);
    }

    public void PostProcessing(bool enable)
    {
        postProcessing.SetActive(enable);
    }
}