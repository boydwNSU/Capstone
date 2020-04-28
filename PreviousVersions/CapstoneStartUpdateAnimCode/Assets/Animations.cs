using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.AI;

public class Animations : MonoBehaviour
{
    
    public Animator manAnimator;
    bool IsWalking = false;
    bool StandingStill = false;
    bool IsBackwardWalking = false;
    bool IsRunning = false;
    bool LeftWalk = false;
    bool RightWalk = false;
    //Testing bools, not meant to be final code
    public bool HasRifle = false;
    public bool CheckRightTurn = false;
    public bool CheckLeftTurn = false;
    public bool IsCrouch = false;
    public bool IsCrouchWalking = false;
    CapsuleCollider manCollider;
    public float MC_Height;
    public float MC_Crouch_Height;
    public bool rigSleep = true;
    public Rigidbody playerRigidbody;

    public ThirdPersonCharacterController playerSpeed;



    // Start is called before the first frame update
    void Start()
    {
        manAnimator =GetComponent<Animator>();//Get the animator

        manCollider = GetComponent<CapsuleCollider>(); //Fetch the capsule collider

        
        MC_Height = 1.85f; //Default collider size
        MC_Crouch_Height = 1.28f; //Crouched collider size

        playerRigidbody.GetComponent<Rigidbody>();

    }
    

    // Update is called once per frame
    void Update()
    {

        //manAnimator.SetBool("StandingStill", StandingStill);
        //manAnimator.SetBool("IsWalking", IsWalking);
        //manAnimator.SetBool("IsBackwardWalking", IsBackwardWalking);
        //manAnimator.SetBool("IsRunning", IsRunning);
        //manAnimator.SetBool("LeftWalk", LeftWalk);
        //manAnimator.SetBool("RightWalk", RightWalk);
        //manAnimator.SetBool("HasRifle", HasRifle);
        //manAnimator.SetBool("CheckRightTurn", CheckRightTurn);
        //manAnimator.SetBool("CheckLeftTurn", CheckLeftTurn);
        //manAnimator.SetBool("IsCrouchWalking", IsCrouchWalking);

        manAnimator.SetBool("IsCrouch", IsCrouch);
        manAnimator.SetFloat("Speed", playerSpeed.Speed);
        manAnimator.SetBool("IsMoving", playerSpeed.isMoving);
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            IsCrouch = true;
            manCollider.height = 1.28f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            IsCrouch = false;
            manCollider.height = MC_Height;
            manCollider.direction = 1;
        }



        /*if (playerSpeed.isMoving == false)
        {
            StandingStill = true;
        }
        if(playerSpeed.isMoving == true)
        {
            StandingStill = false;


        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            IsWalking = true;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsWalking = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            IsBackwardWalking = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            IsBackwardWalking = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            LeftWalk = true;
        }
        if(Input.GetKeyUp(KeyCode.A))
        {
            LeftWalk = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RightWalk = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            RightWalk = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)  playerSpeed.Speed == playerSpeed.RunSpeed)
        {
            IsRunning = true;
            IsWalking = false;
            IsCrouch = false;
        }
        if(playerSpeed.Speed == playerSpeed.CrouchSpeed)
        {
            IsCrouch = true;
            IsWalking = false;
            IsRunning = false;
        }
        if(playerSpeed.Speed == playerSpeed.WalkSpeed)
        {
            IsRunning = false;
            IsCrouch = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsRunning = false;
            IsWalking = true;
        }
        if(IsRunning == true)
        {
            IsWalking = false;
        }
        
        
        //Test ifs for animations, not meant to be final code

        if(Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.W))
        {
            IsCrouchWalking = true;
            IsCrouch = false;
            IsWalking = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && Input.GetKeyUp(KeyCode.W))
        {
            IsCrouchWalking = false;
        }




        if (Input.GetKeyDown(KeyCode.Space))
        {
            HasRifle = true;
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CheckLeftTurn = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            CheckLeftTurn = false;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CheckRightTurn = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            CheckRightTurn = false;
        }*/
        



    }
}
