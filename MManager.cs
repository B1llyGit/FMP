using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MManager : MonoBehaviour
{
    [Header("State Integers")]
    //Overall State Int
    public int State = 1;
    //PC Menu State Int
    public int PCState;
    //KartState
    public int KartState = 0;

    [Header("Camera Components")]
    // Menu Camera
    public GameObject Camera;
    //Cameras Animator
    public Animator CamAnimator;


    [Header("PC Window 1 Components")]
    //Time Trial
    public GameObject TimeTrialButton;
    Animator TimeTrialAnimator;
    //Exhibition
    public GameObject ExhibitionButton;
    Animator ExhibitionAnimator;


    [Header("PC Window 2 Components")]
    // Menu2 Animator
    public GameObject windowTwo;
    Animator windowTwoAnimator;

    [Header("Kart Components")]
    //Karts
    public GameObject KartGroup;
    public Animator KartAnimator;

    private bool IsAxisInUse = false;

    // Start is called before the first frame update
    void Start()
    {
        //Set Camera Anim
        CamAnimator = Camera.GetComponent<Animator>();
        CamAnimator.SetInteger("CamState", 1); //Start

        // Set Time Trial Anim
        TimeTrialAnimator = TimeTrialButton.GetComponent<Animator>();
        TimeTrialAnimator.SetInteger("State", 0);

        // Set Exhibition Anim
        ExhibitionAnimator = ExhibitionButton.GetComponent<Animator>();
        ExhibitionAnimator.SetInteger("State", 0);

        //Set Window 2 Animator
        windowTwoAnimator = windowTwo.GetComponent<Animator>();
        windowTwoAnimator.SetInteger("State", 0);
        windowTwo.SetActive(false);

        //Set Kart Animator
        KartAnimator = KartGroup.GetComponent<Animator>();
        KartAnimator.SetInteger("State", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PCState == 0)
        {
            if (Input.GetButtonDown("Bumper Left") && State > 0)
            {
                State = State -= 1;
            }

            if (Input.GetButtonDown("Bumper Right"))
            {
                State = State += 1;
            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            //Cancelling From Map Select
            if (PCState <= 2)
            {
                PCState = PCState -= 1;
            }
            //Cancelling From Kart Select
            if (PCState == 3)
            {
                PCState = PCState -= 2;
            }
            

        }

        // IF State is "Start"
        if (State == 1)
        {
            CamAnimator.SetInteger("CamState", 1);
        }
        // IF State is "Race Select"
        if (State == 2 && PCState == 0)
        {
            CamAnimator.SetInteger("CamState", 2);
            windowTwoAnimator.SetInteger("State", 1);
        }
        // IF State is Race Select && Time Trial is Chosen
        if (State == 2 && PCState == 1)
        {
            windowTwo.SetActive(true);
            CamAnimator.SetInteger("CamState", 2);
            windowTwoAnimator.SetInteger("State", 0);
            //When going back from kart select
            KartState = 0;
            KartAnimator.SetInteger("State", 0);


        }
        //IF State is Race Select and Track is Being Selected
        if (State == 2 && PCState == 2)
        {
            CamAnimator.SetInteger("CamState", 3);
        }

        //IF State is Race Select and Kart is Being Selected
        if (State == 2 && PCState == 3)
        {
            CamAnimator.SetInteger("CamState", 4);
            if(KartState == 0)
            {
                KartState = 1;
            }

            //Change Active Cart
            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                if ( IsAxisInUse == false)
                {
                    KartState = KartState += 1;
                    IsAxisInUse = true;
                }
                
            }

            //Select First Kart
            if (KartState == 1)
            {
                KartAnimator.SetInteger("State", 1);
            }

            //Select First Kart
            if (KartState == 2)
            {
                KartAnimator.SetInteger("State", 2);
            }

            //Select Second Kart
            if (KartState == 3)
            {
                KartAnimator.SetInteger("State", 3);
            }

            //Create a loop when selecting Karts 
            if (KartState > 3)
            {
                KartState = 1;
            }
        }

        //IF state is "Replays"
        if (State == 3 && PCState == 0)
        {

        }
        //IF state is "Character Customisation"
        if (State == 4)
        {

        }
        //IF state is "Quit"
        if (State == 0)
        {
            CamAnimator.SetInteger("CamState", 0);
        }
        //Button Animations if highlighted
        if (EventSystem.current.currentSelectedGameObject == TimeTrialButton)
        {
            TimeTrialAnimator.SetInteger("State", 1);
        }
        else TimeTrialAnimator.SetInteger("State", 0);

        if (EventSystem.current.currentSelectedGameObject == ExhibitionButton)
        {
            ExhibitionAnimator.SetInteger("State", 1);
        }
        else ExhibitionAnimator.SetInteger("State", 0);

        //Reset the Bool
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            IsAxisInUse = false;
        }

    }


    public void TimeTrialClicked()
    {
        PCState = 1;
    }

    public void MapSelectClicked()
    {
        PCState = 2;
    }

    public void KartSelectClicked()
    {
        PCState = 3;
    }
}
