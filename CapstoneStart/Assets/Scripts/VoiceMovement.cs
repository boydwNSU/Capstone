using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEditor;
using System.Linq;
using System;

public class VoiceMovement : MonoBehaviour
{
    public string[] keywords = new string[] { "turn around", "left", "right", "stop", "crouch", "stand", "run", "walk", "keyboard", "voice", "Point of View",
                                              "Help", "Cancel","back", "Manual Control", "Voice Control", "First Person", "Third Person","Control Method"}; //All of the words recognized by the Voice Recognition
    public ConfidenceLevel confidence = ConfidenceLevel.Low; //How sure the computer is that it is hearing the correct word
    public KeywordRecognizer recognizer; //The voice recognition
    //public float speed = .05f;
    protected string word = ""; //The word that is being said
    public GameObject target;

    //REferences to other scripts
    public Animations animationVariables;
    public ThirdPersonCharacterController playerSpeed;
    public HeadBob Headbob;
    public HeadBob VoiceHeadBob;

    public GameObject KeyboardCamera; //Camera used for Manual controls
    public GameObject VoiceRecogCamera; //Camera used for Voice controls
    public GameObject CameraAnchorFirstPerson; //Anchor point for first person camera view
    public GameObject CameraAnchorThirdPerson; //Anchor point for third person camera view

    //GameObjects for pieces of the model
    public GameObject ModelEyes;
    public GameObject ModelFace;
    public GameObject ModelHat;
    public GameObject ModelHair;

    float horizontalMovementInput = 0f; //Movement on the X Axis
    float verticalMovementInput = 0f; //Movement on the Z axis
    bool hasTurned = false; //Bool used in keyword recognition to allow an action to be done only once when keyword is said (Very important)
    bool isRunning = false; //Checks if the player is running

    private float startAngle;
    private float targetAngle;
    private float startTime;
    float smooth = 1.0f; // The number of seconds taken to rotate
    private float angle;

    //Game objects for the different panels of the help menu
    public GameObject HelpMenu;
    public GameObject ControlMethodMenu;
    public GameObject POVMenu;
    
    

    // Start is called before the first frame update
    void Start()
    {
        angle = transform.eulerAngles.y;
        
        if (keywords != null)
        {
            recognizer = new KeywordRecognizer(keywords, confidence);
            recognizer.OnPhraseRecognized += OnPhraseRecognized;
            recognizer.Start();
        }
    }



    /*
     * verticalMovementInput = 1f is forward
     * verticalMovementInput = -1f is back
     * horizontalMovementInput = -1f is left
     * horizontalMovementInput = 1f is right
     * */

    // Update is called once per frame
    void Update()
    {
        switch (word)
        {
            case "turn around":
                if(hasTurned == true) //Only done once
                {
                    startTime = Time.time;
                    startAngle = angle;
                    targetAngle = targetAngle + 180;
                }

                angle = Mathf.LerpAngle(startAngle, targetAngle, (Time.time - startTime) / smooth);
                transform.eulerAngles = new Vector3(0, angle, 0);

                hasTurned = false;
                break;

            case "left":
                if(hasTurned == true)
                {
                    startTime = Time.time;
                    startAngle = angle;
                    targetAngle = targetAngle - 90;
                }

                angle = Mathf.LerpAngle(startAngle, targetAngle, (Time.time - startTime) / smooth);
                transform.eulerAngles = new Vector3(0, angle, 0);

                hasTurned = false;
                break;

            case "right":
                if (hasTurned == true)
                {
                    startTime = Time.time;
                    startAngle = angle;
                    targetAngle = targetAngle + 90;

                }

                angle = Mathf.LerpAngle(startAngle, targetAngle, (Time.time - startTime) / smooth);
                transform.eulerAngles = new Vector3(0, angle, 0);
                
                hasTurned = false;
                break;
            case "stop":
                ResetVariables();
                playerSpeed.Speed = playerSpeed.WalkSpeed;
                break;

            case "crouch":
                animationVariables.IsCrouch = true;
                animationVariables.manCollider.height = animationVariables.MC_Crouch_Height;
                DoAnimations();
                Headbob.crouching = true;
                VoiceHeadBob.crouching = true;

                if (Headbob.crouching == true && Headbob.isFirstPerson == true)
                {
                    KeyboardCamera.transform.position = Headbob.crouchCameraAnchor.transform.position;
                    VoiceRecogCamera.transform.position = Headbob.crouchCameraAnchor.transform.position;
                }
                break;

            case "stand":
                animationVariables.IsCrouch = false;
                animationVariables.manCollider.height = animationVariables.MC_Height;
                playerSpeed.Speed = playerSpeed.WalkSpeed;
                Headbob.crouching = false;
                VoiceHeadBob.crouching = false;
                if (verticalMovementInput == 1f)
                {
                    DoAnimations();
                }

                if (Headbob.crouching == false && Headbob.isFirstPerson == true)
                {
                    KeyboardCamera.transform.position = CameraAnchorFirstPerson.transform.position;
                    VoiceRecogCamera.transform.position = CameraAnchorFirstPerson.transform.position;
                }
                break;

            case "run":
                isRunning = true;
                DoAnimations();
                Headbob.running = true;
                VoiceHeadBob.running = true;
                break;

            case "walk":
                isRunning = false;
                animationVariables.IsCrouch = false;
                DoAnimations();
                Headbob.running = false;
                VoiceHeadBob.running = false;
                break;

            case "Help":
                HelpMenu.gameObject.SetActive(true);
                break;

            case "Cancel":
                HelpMenu.gameObject.SetActive(false);
                break;

            case "Point of View":
                if(ControlMethodMenu.gameObject.activeSelf)
                {
                    break;
                }
                POVMenu.gameObject.SetActive(true);
                HelpMenu.gameObject.SetActive(false);
                break;

            case "Control Method":
                if(POVMenu.gameObject.activeSelf)
                {
                    break;
                }
                ControlMethodMenu.gameObject.SetActive(true);
                HelpMenu.gameObject.SetActive(false);
                break;

            case "back":
                HelpMenu.gameObject.SetActive(true);
                ControlMethodMenu.gameObject.SetActive(false);
                POVMenu.gameObject.SetActive(false);
                break;

            case "Voice Control":
                if (hasTurned == true)
                {
                    KeyboardCamera.SetActive(false);
                    VoiceRecogCamera.SetActive(true);
                }
                break;

            case "Manual Control":
                if (hasTurned == true)
                {
                    KeyboardCamera.SetActive(true);
                    VoiceRecogCamera.SetActive(false);
                }
                break;

            case "First Person":
                if(hasTurned == true)
                {
                    KeyboardCamera.transform.position = CameraAnchorFirstPerson.transform.position;
                    VoiceRecogCamera.transform.position = CameraAnchorFirstPerson.transform.position;
                    
                    ModelEyes.SetActive(false);
                    ModelFace.SetActive(false);
                    ModelHat.SetActive(false);
                    ModelHair.SetActive(false);
                    Headbob.isFirstPerson = true;
                    VoiceHeadBob.isFirstPerson = true;
                }
                break;

            case "Third Person":
                if (hasTurned == true)
                {
                    KeyboardCamera.transform.position = CameraAnchorThirdPerson.transform.position;
                    VoiceRecogCamera.transform.position = CameraAnchorThirdPerson.transform.position;
                    ModelEyes.SetActive(true);
                    ModelFace.SetActive(true);
                    ModelHat.SetActive(true);
                    ModelHair.SetActive(true);
                    Headbob.isFirstPerson = false;
                    VoiceHeadBob.isFirstPerson = false;
                }
                break;
        }

        
        Vector3 playerMovement = new Vector3(horizontalMovementInput, 0f, verticalMovementInput) * playerSpeed.Speed * Time.deltaTime; //allows character to move across terrain
        transform.Translate(playerMovement, Space.Self); //actually moves the character

    }
    

    public void DoAnimations()
    {
        if (animationVariables.IsCrouch == true)
        {
            playerSpeed.Speed = playerSpeed.CrouchSpeed;
        }
        else if (isRunning == true)
        {
            playerSpeed.Speed = playerSpeed.RunSpeed;
            verticalMovementInput = 1f;
        }
        else
        {
            playerSpeed.Speed = playerSpeed.WalkSpeed;
            animationVariables.WalkForward = true;
            verticalMovementInput = 1f;
        }

        
        
    }
    
    public void ResetVariables() //Sets all variables back to dafault state
    {
        verticalMovementInput = 0f;
        horizontalMovementInput = 0f;
        animationVariables.WalkRight = false;
        animationVariables.WalkLeft = false;
        animationVariables.WalkBackward = false;
        animationVariables.WalkForward = false;
    }
    
    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        word = args.text;
        Debug.Log("You said: <b>" + word + "</b>");
        hasTurned = true;
    }

    private void OnApplicationQuit()
    {
        if (recognizer != null && recognizer.IsRunning)
        {
            recognizer.OnPhraseRecognized -= OnPhraseRecognized;
            recognizer.Stop();
        }
    }
    
}
 
 
 
 