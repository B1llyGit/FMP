using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class MenuManager : MonoBehaviour
{
    [Header("Camera")]
    public GameObject Camera;
    public Animator CamAnim;

    [Header("StartMenu")]
    public GameObject StartMenu;
    public Button RaceStart;
    public GameObject StartText;

    [SerializeField]
    int CamState = 0;

    //Player Input Actions
    private PlayerInput playerInput;
    // Reference to pressing B on Controller
    private InputAction cancelAction;

    [Header("Race Window")]
    public Button timeTrialButton;
    // Animator timeTrialAnimator;
    public Button raceButton;
    // Animator raceAnimator;

    [Header("Time Trial Window")]
    public GameObject timeTrialSetupWindow;
    Animator timeTrialSetupWindowAnimator;
    public Button timeTrialSetupRace;

    public int MenuState;

    [Header("Track Select")]
    public Button TrackOne;

    [Header("Kart Select")]
    public Button KartForwards;


    [Header("Character Select")]
    public Button HeadFowards;

    [Header("Settings")]
    public Toggle FullScreen;
    public Dropdown Quality;
    public Dropdown Resolution;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>(); // Reference to Player Input

        cancelAction = playerInput.actions["Cancel"]; // Reference to pressing B on Controller
        cancelAction.performed += Cancel;
    }

    void Start()
    {
        CamAnim = Camera.GetComponent<Animator>(); //Set Camera Anim
        CamAnim.SetInteger("State", CamState);

        timeTrialSetupWindowAnimator = timeTrialSetupWindow.GetComponent<Animator>();
    }

    void Update()
    {
        CamAnim.SetInteger("State", CamState);


    }

    public void Race() //To Race From the Start Menu
    {

        CamState = 1; // Move the Camera infront of the PC
        StartMenu.SetActive(false); // Disable Intial Menu UI
        timeTrialButton.Select();



    }

    public void Replay() //To Replays From the Start Menu
    {
        CamState = 2;
        StartMenu.SetActive(false);
    }

    public void Settings()
    {
        CamState = 4;
        StartMenu.SetActive(false);
        //Select First Settings button
        FullScreen.Select();
    }

    public void Quit() //When Quitting From the Start Menu
    {
        CamState = 3;
        StartMenu.SetActive(false);
    }

    public void Cancel(InputAction.CallbackContext context)     //Pressing B
    {
        if (context.performed == true)
        {
            if (CamState >= 1 && CamState <= 4) //if the camera is currently positioned in one of the 4 avenues 
            {
                if (MenuState == 0)  // If the time trial Window isnt active - No mini stuff is active
                {
                    CamState = 0;
                    StartMenu.SetActive(true);
                    StartText.SetActive(true);
                    RaceStart.Select();
                    Debug.LogWarning("State Set to 0");
                }

                if (MenuState == 1) // if the Time Trial Window Window is active
                {
                    timeTrialSetupWindow.SetActive(false);
                    timeTrialButton.Select();
                    MenuState = 0;
                }

                if (MenuState == 2) // if one of the Settings dropdowns is currently selected
                {
                    //FullScreen.Select();
                    Quality.Hide();
                    Resolution.Hide();
                }
            }
            if (CamState == 5) // if the camera is looking at the map select
            {
                    CamState = 1;
                    Debug.LogWarning("State Set to 5 ");
                    timeTrialSetupRace.Select();

            }

            if (CamState == 6) // if the camera is looking at Kart Select
            {
                    CamState = 1;
                    Debug.LogWarning("State Set to 6");
                    timeTrialSetupRace.Select();

            }

            if (CamState == 7) // if the camera is looking at Character Select
            {
                    CamState = 1;
                    Debug.LogWarning("State Set to 7");
                    timeTrialSetupRace.Select();

            }


            
        }
    }

        public void TimeTrialSetup()
        {
            timeTrialSetupWindow.SetActive(true);
            MenuState = 1;
            timeTrialSetupRace.Select();

        }

        public void KartSelect()
        {
            CamState = 6;
            KartForwards.Select();
        }

        public void MapSelect()
        {
            CamState = 5;
            TrackOne.Select();
        }

        public void CharacterSelect()
        {
            CamState = 7;
            HeadFowards.Select();
        }

        public void KartConfirm()
        {
            CamState = 1;
            timeTrialSetupRace.Select();
            // Save Karts Setup Here
        }
}



