using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public Animator manAnimator;
    public bool IsWalking = false;
    public bool StandingStill = false;
    public bool IsBackwardWalking = false;
    public bool IsRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        //Get the animator
        manAnimator =GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        manAnimator.SetBool("StandingStill", StandingStill);
        manAnimator.SetBool("IsWalking", IsWalking);
        manAnimator.SetBool("IsBackwardWalking", IsBackwardWalking);
        manAnimator.SetBool("IsRunning", IsRunning);

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
        


    }
}
