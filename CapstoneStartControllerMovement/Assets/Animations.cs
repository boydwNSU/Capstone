using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.AI;
using UnityEditor;

public class Animations : MonoBehaviour
{
    public Animator manAnimator;
    public ThirdPersonCharacterController playerSpeed;
    public ThirdPersonCharacterController ControllerDirection;
    public ThirdPersonCameraController CheckForController;
    CapsuleCollider manCollider;

    public float MC_Height;
    public float MC_Crouch_Height;

    public bool WalkBackward = false;
    public bool WalkForward = false;
    public bool IsCrouch = false;
    public bool WalkLeft = false;
    public bool WalkRight = false;
    public bool TurnRight = false;
    public bool TurnLeft = false;


    /*
    Joystick buttons:
        joystick 1 button 0 = A
        joystick 1 button 1 = B
        joystick 1 button 2 = X
        joystick 1 button 3 = Y
        joystick 1 button 4 = LB
        joystick 1 button 5 = RB
        joystick 1 button 6 = Back
        joystick 1 button 7 = Start
        joystick 1 button 8 = Press L Joystick
        joystick 1 button 9 = Press R Joystick
        joystick 1 button 10 = 
        joystick 1 button 11 = 
        joystick 1 button 12 = 
         */




    // Start is called before the first frame update
    void Start()
    {
        manAnimator =GetComponent<Animator>();//Get the animator

        manCollider = GetComponent<CapsuleCollider>(); //Fetch the capsule collider
        
        MC_Height = 1.85f; //Default collider size
        MC_Crouch_Height = 1.28f; //Crouched collider size
        
        
    }
    

    // Update is called once per frame
    void Update()
    {
        

        if (CheckForController.isControllerConnected == false)
        {
            KeyboardMovement();
        }
        if (CheckForController.isControllerConnected == true)
        {
            ControllerMovement();
        }


        manAnimator.SetBool("WalkLeft", WalkLeft);
        manAnimator.SetBool("WalkRight", WalkRight);
        manAnimator.SetBool("IsCrouch", IsCrouch);
        manAnimator.SetFloat("Speed", playerSpeed.Speed);
        manAnimator.SetBool("IsMoving", playerSpeed.isMoving);
        manAnimator.SetBool("WalkBackward", WalkBackward);
        manAnimator.SetBool("WalkForward", WalkForward);
        manAnimator.SetBool("TurnRight", TurnRight);
        manAnimator.SetBool("TurnLeft", TurnLeft);

    }
    void KeyboardMovement()
    {
        if(Input.GetAxis("Mouse X") > 0) //turn right
        {
            TurnRight = true;
            TurnLeft = false;
        }
        if (Input.GetAxis("Mouse X") == 0) //Not turning
        {
            TurnRight = false;
            TurnLeft = false;
        }
        if (Input.GetAxis("Mouse X") < 0) //turn left
        {
            TurnRight = false;
            TurnLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            IsCrouch = true;
            manCollider.height = MC_Crouch_Height;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            IsCrouch = false;
            manCollider.height = MC_Height;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            WalkBackward = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            WalkBackward = false;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            WalkForward = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            WalkForward = false;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            WalkLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            WalkLeft = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            WalkRight = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            WalkRight = false;
        }
    }
    void ControllerMovement()
    {
        if (Input.GetAxis("LeftStickVertical") > 0) //turn right
        {
            TurnRight = true;
            TurnLeft = false;
        }
        if (Input.GetAxis("LeftStickVertical") == 0) //Not turning
        {
            TurnRight = false;
            TurnLeft = false;
        }
        if (Input.GetAxis("LeftStickVertical") < 0) //turn left
        {
            TurnRight = false;
            TurnLeft = true;
        }
        if (ControllerDirection.verticalMovementInput == -1)
        {
            WalkBackward = true;
        }
        if (ControllerDirection.verticalMovementInput != -1)
        {
            WalkBackward = false;
        }
        if (ControllerDirection.verticalMovementInput == 1)
        {
            WalkForward = true;
        }
        if (ControllerDirection.verticalMovementInput != 1)
        {
            WalkForward = false;
        }
        if (ControllerDirection.horizontalMovementInput == -1)
        {
            WalkLeft = true;
        }
        if (ControllerDirection.horizontalMovementInput != -1)
        {
            WalkLeft = false;
        }
        if (ControllerDirection.horizontalMovementInput == 1)
        {
            WalkRight = true;
        }
        if (ControllerDirection.horizontalMovementInput != 1)
        {
            WalkRight = false;
        }
        if(Input.GetKeyDown("joystick 1 button 1")) //If B is held down
        {
            IsCrouch = true;
            manCollider.height = MC_Crouch_Height;
        }
        if(Input.GetKeyUp("joystick 1 button 1")) //If B is let up
        {
            IsCrouch = false;
            manCollider.height = MC_Height;
        }
        
        //Trying for toggle crouch
        /*
        if (Input.GetKey("joystick 1 button 9"))
        {
            if (IsCrouch == false)
            {
                Debug.Log("Test");
                IsCrouch = true;
                manCollider.height = MC_Crouch_Height;
            }
            if (IsCrouch == true)
            {
                IsCrouch = false;
                manCollider.height = MC_Height;
            }
        }
        */
        
    }
}
