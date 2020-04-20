using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public float walkingBobbingSpeed = 4f;
    public float runningBobSpeed = 8f;
    public float crouchBobSpeed = 2f;
    public float currentBobSpeed;
    public float bobbingAmount = 0.1f;
    float runningBobAmount = 0.2f;
    float walkingBobAmount = 0.1f;
    public ThirdPersonCharacterController controller;
    public VoiceMovement POV;

    public bool running;
    public bool crouching = false;

    public GameObject firstPersonAnchor;
    public GameObject crouchCameraAnchor;

    public float defaultPosY = 0;
    float timer = 0;
    public bool isFirstPerson;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = firstPersonAnchor.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("isFirstPerson in Update is " + isFirstPerson);
        if (isFirstPerson == true)
        {
            BobHead();
        }
    }

    public void BobHead()
    {
        if (controller.isMoving == true)
        {
            //Player is moving
            if(running == true)
            {
                currentBobSpeed = runningBobSpeed;
                bobbingAmount = runningBobAmount;
            }
            if(crouching == true)
            {
                currentBobSpeed = crouchBobSpeed;
                POV.KeyboardCamera.transform.position = crouchCameraAnchor.transform.position;
                POV.VoiceRecogCamera.transform.position = crouchCameraAnchor.transform.position;
            }
            else if (running == false)
            {
                currentBobSpeed = walkingBobbingSpeed;
                bobbingAmount = walkingBobAmount;
                POV.KeyboardCamera.transform.position = POV.CameraAnchorFirstPerson.transform.position;
                POV.VoiceRecogCamera.transform.position = POV.CameraAnchorFirstPerson.transform.position;
            }


            timer += Time.deltaTime * currentBobSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
                
        }
        else
        {
            //Idle
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * currentBobSpeed), transform.localPosition.z);
                
        }
    }
}
