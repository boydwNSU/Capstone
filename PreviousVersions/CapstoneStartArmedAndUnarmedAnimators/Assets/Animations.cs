using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    CapsuleCollider manCollider;
    float MC_Height;



    // Start is called before the first frame update
    void Start()
    {
        manAnimator =GetComponent<Animator>();//Get the animator

        manCollider = GetComponent<CapsuleCollider>(); //Fetch the capsule collider

        //Default collider size
        MC_Height = 1.85f;
        
    }

    // Update is called once per frame
    void Update()
    {
        manAnimator.SetBool("StandingStill", StandingStill);
        manAnimator.SetBool("IsWalking", IsWalking);
        manAnimator.SetBool("IsBackwardWalking", IsBackwardWalking);
        manAnimator.SetBool("IsRunning", IsRunning);
        manAnimator.SetBool("LeftWalk", LeftWalk);
        manAnimator.SetBool("RightWalk", RightWalk);
        //Test code, not meant to be final code
        manAnimator.SetBool("HasRifle", HasRifle);
        manAnimator.SetBool("CheckRightTurn", CheckRightTurn);
        manAnimator.SetBool("CheckLeftTurn", CheckLeftTurn);
        manAnimator.SetBool("IsCrouch", IsCrouch);

        if (!Input.anyKey)
        {
            StandingStill = true;
        }
        if(Input.anyKey)
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            IsRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsRunning = false;
        }
        if(IsRunning == true)
        {
            IsWalking = false;
        }
        
        //Test ifs for animations, not meant to be final code
        if(Input.GetKeyDown(KeyCode.Space))
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
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            IsCrouch = true;
            IsWalking = false;
            manCollider.height = 1.28f;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            IsCrouch = false;
            manCollider.height = MC_Height;
            manCollider.direction = 1;
        }



    }
}
