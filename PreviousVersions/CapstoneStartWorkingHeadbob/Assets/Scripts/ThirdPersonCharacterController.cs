using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThirdPersonCharacterController : MonoBehaviour
{
    public Collider manCollider;
    public Rigidbody playerRigidbody;
    public ThirdPersonCameraController CheckForController;
    public GameObject HelpMenu;
    public HeadBob Headbob;
    public HeadBob VoiceHeadbob;
    public VoiceMovement VoiceMovement;

    public float horizontalMovementInput;
    public float verticalMovementInput;


    public float Speed = 1f; //Character's move speed
    public float RunSpeed = 5f; //Character speed while running
    public float WalkSpeed = 1f; //Character speed while walking
    public float CrouchSpeed = .5f; //Character speed while crouched

    //Check if player is moving
    public Transform playerTransform;
    private float noMovementThreshold = 0.0001f;
    private const int noMovementFrames = 3;
    Vector3[] previousLocations = new Vector3[noMovementFrames];
    public bool isMoving; //Checks if the player is moving

    void Awake() //Sets previous locations
    {
        for(int i = 0; i < previousLocations.Length; i++)
        {
            previousLocations[i] = Vector3.zero;
        }
    }
    //End Check if player is moving in this area

    void Start()
    {
        Awake();
    }

    // Update is called once per frame
    void Update()
    {
        IsMoving();

        if (CheckForController.isControllerConnected == false)
        {
            PlayerMovement();
        }
        if (CheckForController.isControllerConnected == true)
        {
            ControllerMovement();
        }

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick 1 button 7"))
        {
            //HelpMenuSets.HelpMenu.SetActive(true);

            if(HelpMenu.activeSelf)
            {
                HelpMenu.SetActive(false);
                Cursor.visible = false; //Removes cursor
                Cursor.lockState = CursorLockMode.Locked; //Locks cursor to center of screen
            }
            else
            {
                HelpMenu.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        

    }

    void IsMoving()
    {
        //Check if moving
        //Store the newest vector at the end of the list of vectors
        for (int i = 0; i < previousLocations.Length - 1; i++)
        {
            previousLocations[i] = previousLocations[i + 1];
        }
        previousLocations[previousLocations.Length - 1] = playerTransform.position;

        //Check the distances between the points in your previous locations
        //If for the past several updates, there are no movements smaller than the threshold,
        //you can most likely assume that the object is not moving
        for (int i = 0; i < previousLocations.Length - 1; i++)
        {
            if (Vector3.Distance(previousLocations[i], previousLocations[i + 1]) >= noMovementThreshold)
            {
                //The minimum movement has been detected between frames
                isMoving = true;
                //gunPositions.gunIdle.SetActive(false);
                break;
            }
            else
            {
                isMoving = false;
                //gunPositions.gunIdle.SetActive(true);
            }
        }
    }

    

    void PlayerMovement()
    {
        horizontalMovementInput = Input.GetAxis("Horizontal"); //movement input for horizontal
        verticalMovementInput = Input.GetAxis("Vertical"); //movement input for vertical

        Vector3 playerMovement = new Vector3(horizontalMovementInput, 0f, verticalMovementInput) * Speed * Time.deltaTime; //allows character to move across terrain
        transform.Translate(playerMovement, Space.Self); //actually moves the character

        if(Input.GetKeyDown(KeyCode.LeftShift) && isMoving == true) //If the left shift is held down
        {
            Speed = RunSpeed;
            Headbob.running = true;
            VoiceHeadbob.running = true;
            
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) && isMoving == true)
        {
            Speed = WalkSpeed;
            Headbob.running = false;
            VoiceHeadbob.running = false;
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            Speed = CrouchSpeed;
            Headbob.crouching = true;
            
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            Speed = WalkSpeed;
            Headbob.crouching = false;
        }
        
        /*
        if (Input.GetKey("joystick 1 button 9"))
        {
            
            if (Speed == CrouchSpeed)
            {
                Speed = WalkSpeed;
                Debug.Log("Speed up");
            }
            if (Speed == WalkSpeed)
            {
                Speed = CrouchSpeed;
                Debug.Log("Speed down");
            }
        }*/


    }
    void ControllerMovement()
    {
        horizontalMovementInput = Input.GetAxis("xAxis"); //movement input for horizontal
        verticalMovementInput = Input.GetAxis("yAxis"); //movement input for vertical

        Vector3 playerMovement = new Vector3(horizontalMovementInput, 0f, verticalMovementInput) * Speed * Time.deltaTime; //allows character to move across terrain
        transform.Translate(playerMovement, Space.Self); //actually moves the character

        if (Input.GetKeyDown("joystick 1 button 1"))
        {
            Speed = CrouchSpeed;
        }
        if (Input.GetKeyUp("joystick 1 button 1"))
        {
            Speed = WalkSpeed;
        }
        if (Input.GetKeyDown("joystick 1 button 0"))
        {
            Speed = RunSpeed;
            Headbob.running = true;
            VoiceHeadbob.running = true;
        }
        if (Input.GetKeyUp("joystick 1 button 0"))
        {
            Speed = WalkSpeed;
            Headbob.running = false;
            VoiceHeadbob.running = false;
        }
    }
    

    
}
