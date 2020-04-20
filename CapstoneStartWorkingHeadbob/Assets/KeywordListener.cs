using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEditor;
using System.Linq;
using System;

public class KeywordListener : MonoBehaviour
{
    public string[] keywords = new string[] { "forward", "back", "left", "right", "stop", "crouch", "stand", "run" };
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

    // Update is called once per frame
    void Update()
    {
        switch (word)
        {
            case "forward":
                ResetVariables();
                verticalMovementInput = 1f;
                animationVariables.WalkForward = true;

                if (animationVariables.IsCrouch == true)
                {
                    playerSpeed.Speed = playerSpeed.CrouchSpeed;
                }
                else
                {
                    playerSpeed.Speed = playerSpeed.WalkSpeed;
                }
                break;
            case "back":
                ResetVariables();
                verticalMovementInput = -1f;
                animationVariables.WalkBackward = true;
                break;
            case "left":
                ResetVariables();
                horizontalMovementInput = -1f;
                animationVariables.WalkLeft = true;
                break;
            case "right":
                ResetVariables();
                horizontalMovementInput = 1f;
                animationVariables.WalkRight = true;
                break;
            case "stop":
                ResetVariables();
                break;
            case "crouch":
                playerSpeed.Speed = playerSpeed.CrouchSpeed;
                animationVariables.IsCrouch = true;
                animationVariables.manCollider.height = animationVariables.MC_Crouch_Height;
                break;
            case "stand":
                playerSpeed.Speed = playerSpeed.WalkSpeed;
                animationVariables.IsCrouch = false;
                animationVariables.manCollider.height = animationVariables.MC_Height;
                break;
            case "run":
                
                if(animationVariables.WalkForward == true)
                {
                    playerSpeed.Speed = playerSpeed.RunSpeed;
                }
                else
                {
                    playerSpeed.Speed = playerSpeed.WalkSpeed;
                }
                break;

        }


        /*
        switch (word)
        {
            case "forward":
                verticalMovementInput = 1f;
                horizontalMovementInput = 0f;
                animationVariables.WalkForward = true;
                animationVariables.WalkRight = false;
                animationVariables.WalkLeft = false;
                animationVariables.WalkBackward = false;

                if (animationVariables.IsCrouch == true)
                {
                    playerSpeed.Speed = playerSpeed.CrouchSpeed;
                }
                else
                {
                    playerSpeed.Speed = playerSpeed.WalkSpeed;
                }
                break;
            case "back":
                verticalMovementInput = -1f;
                horizontalMovementInput = 0f;
                animationVariables.WalkBackward = true;
                animationVariables.WalkRight = false;
                animationVariables.WalkLeft = false;
                animationVariables.WalkForward = false;
                break;
            case "left":
                verticalMovementInput = 0f;
                horizontalMovementInput = -1f;
                animationVariables.WalkLeft = true;
                animationVariables.WalkRight = false;
                animationVariables.WalkBackward = false;
                animationVariables.WalkForward = false;
                break;
            case "right":
                verticalMovementInput = 0f;
                horizontalMovementInput = 1f;
                animationVariables.WalkRight = true;
                animationVariables.WalkLeft = false;
                animationVariables.WalkBackward = false;
                animationVariables.WalkForward = false;
                break;
            case "stop":
                verticalMovementInput = 0f;
                horizontalMovementInput = 0f;
                animationVariables.WalkRight = false;
                animationVariables.WalkLeft = false;
                animationVariables.WalkBackward = false;
                animationVariables.WalkForward = false;
                break;
            case "crouch":
                playerSpeed.Speed = playerSpeed.CrouchSpeed;
                animationVariables.IsCrouch = true;
                animationVariables.manCollider.height = animationVariables.MC_Crouch_Height;
                break;
            case "stand":
                playerSpeed.Speed = playerSpeed.WalkSpeed;
                animationVariables.IsCrouch = false;
                animationVariables.manCollider.height = animationVariables.MC_Height;
                break;
            case "run":
                playerSpeed.Speed = playerSpeed.RunSpeed;
                break;
        }
        */
        Vector3 playerMovement = new Vector3(horizontalMovementInput, 0f, verticalMovementInput) * playerSpeed.Speed * Time.deltaTime; //allows character to move across terrain
        transform.Translate(playerMovement, Space.Self); //actually moves the character

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
        Debug.Log( "You said: <b>" + word + "</b>");
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
