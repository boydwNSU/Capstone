using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEditor;
using System.Linq;
using System;

public class VoiceMovement : MonoBehaviour
{
    public string[] keywords = new string[] { "turn around", "left", "right", "stop", "crouch", "stand", "run", "walk" };
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public KeywordRecognizer recognizer;
    public float speed = .05f;
    protected string word = "";
    public GameObject target;
    public Animations animationVariables;
    public ThirdPersonCharacterController playerSpeed;

    public float horizontalMovementInput = 0f;
    public float verticalMovementInput = 0f;
    public Transform playerTransform;


    public GameObject modelTransform;
    
    public float rotateRight = 90.0f;
    public float rotateLeft = 90.0f;
    public float rotateBack = 180.0f;
    public bool hasTurned = false;
    public bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {

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
                if(hasTurned == true)
                {
                    RotateSoldier(rotateBack);
                }

                hasTurned = false;
                break;
            case "left":
                if(hasTurned == true)
                {
                    RotateSoldier(rotateLeft);
                }
                hasTurned = false;
                break;
            case "right":
                if (hasTurned == true)
                {
                    RotateSoldier(rotateRight);
                }
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
                break;
            case "stand":
                animationVariables.IsCrouch = false;
                animationVariables.manCollider.height = animationVariables.MC_Height;
                playerSpeed.Speed = playerSpeed.WalkSpeed;
                if(verticalMovementInput == 1f)
                {
                    DoAnimations();
                }
                
                
                break;
            case "run":
                isRunning = true;
                DoAnimations();
                break;
            case "walk":
                isRunning = false;
                animationVariables.IsCrouch = false;
                DoAnimations();
                break;


        }

        
        Vector3 playerMovement = new Vector3(horizontalMovementInput, 0f, verticalMovementInput) * playerSpeed.Speed * Time.deltaTime; //allows character to move across terrain
        transform.Translate(playerMovement, Space.Self); //actually moves the character

    }

    public void RotateSoldier(float rotationDegree)
    {
        target.transform.Rotate(0.0f, rotationDegree, 0.0f, Space.Self);
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
    

    public void ResetVariables()
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
 
 
 
 