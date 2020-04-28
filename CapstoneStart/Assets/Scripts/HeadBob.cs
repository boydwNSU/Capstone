using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    //These control the speed and intensity of the camera bob
    public float walkingBobbingSpeed = 4f;
    public float runningBobSpeed = 8f;
    public float crouchBobSpeed = 2f;
    public float currentBobSpeed;
    public float bobbingAmount = 0.1f;
    float runningBobAmount = 0.2f;
    float walkingBobAmount = 0.1f;

    //References to other scripts
    public ThirdPersonCharacterController controller;
    public VoiceMovement POV;

    public bool isFirstPerson; //Determines if the player is in first person
    public bool running; //Determines if the player is running
    public bool crouching = false; //Determines if the player is crouching

    //Used to move the camera from a standing to a crouching position and back
    public GameObject firstPersonAnchor;  //The anchor object set at the first person standing level
    public GameObject crouchCameraAnchor; // The anchor object set at the first person crouching level
    
    public float defaultPosY = 0; //Determines the starting position of the camera bob on the Y axis
    float timer = 0; //Used to start the bob
    

    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = firstPersonAnchor.transform.localPosition.y; //Sets the defaultPosY variable to the Y axis coordinate of the firstPersonAnchor GameObject
    }

    // Update is called once per frame
    void Update()
    {

        //If the player is in first person the call the BobHead function
        if (isFirstPerson == true)
        {
            BobHead();
        }
    }

    public void BobHead() //Used to make the camera bob
    {
        if (controller.isMoving == true) //If the player is moving
        {
            if(running == true)
            {
                //Sets the bob speed and intensity to the running levels and sets the DefaultPosY to the firstPersonAnchor Y coordinate
                currentBobSpeed = runningBobSpeed;
                bobbingAmount = runningBobAmount;
                defaultPosY = firstPersonAnchor.transform.localPosition.y;
            }
            if (crouching == true)
            {
                //Sets the camera location to the crouchCameraAnchor position
                POV.KeyboardCamera.transform.position = crouchCameraAnchor.transform.position;
                POV.VoiceRecogCamera.transform.position = crouchCameraAnchor.transform.position;
                
                //Sets the bob speed and intensity to crouching levels and sets the defaultPosY to the crouchCameraAnchor Y coordinate
                currentBobSpeed = crouchBobSpeed;
                bobbingAmount = walkingBobAmount;
                defaultPosY = crouchCameraAnchor.transform.localPosition.y;
            }
            else if (running == false && crouching == false) //If player is not running or crouching
            {
                //Sets the bob speed and intensity to the walking levels and sets the DefaultPosY to the firstPersonAnchor Y coordinate
                currentBobSpeed = walkingBobbingSpeed;
                bobbingAmount = walkingBobAmount;
                defaultPosY = firstPersonAnchor.transform.localPosition.y;
            }
            
            timer += Time.deltaTime * currentBobSpeed; //Sets the bob speed based on the currentBobSpeed value
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) 
                * bobbingAmount, transform.localPosition.z); //Actually bobs the camera 
        }
        else
        {
            //Idle. Stops the camera bob when player is not moving
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, 
                Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * currentBobSpeed), transform.localPosition.z);
                
        }
    }
}
