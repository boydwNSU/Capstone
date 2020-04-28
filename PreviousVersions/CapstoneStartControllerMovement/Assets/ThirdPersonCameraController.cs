using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public float rotationSpeed = 1; //the speed of rotation
    public Transform Target, Player;
    public float mouseX, mouseY;
    public float joystickX, joystickY;

    public bool isControllerConnected = false;

    void Start()
    {
        Cursor.visible = false; //Removes cursor
        Cursor.lockState = CursorLockMode.Locked; //Locks cursor to center of screen
    }

    void Update()
    {
        

        string[] names = Input.GetJoystickNames();
        for(int i = 0; i < names.Length; i++)
        {
            if(names[i].Length > 1)
            {
                isControllerConnected = true;
            }
        }

        if(isControllerConnected == true)
        {
            ControllerCamera();
        }
        else
        {
            CamControl();
        }
    }

    void CamControl() //Controls the camera
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed; //Controls camera moving left and right
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed; //Controls camera moving up and down
        mouseY = Mathf.Clamp(mouseY, -35, 60); //Prevent cam from flipping all the way aroud the player up and down
        
        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);//Allows cam to rotate
        Player.rotation = Quaternion.Euler(0, mouseX, 0); //Allows player to rotate
        
        transform.LookAt(Target); //Camera focused on correct area
    }

    void ControllerCamera()
    {
        joystickX += Input.GetAxis("LeftStickVertical") * rotationSpeed;
        joystickY -= Input.GetAxis("LeftStickHorizontal") * rotationSpeed;
        joystickY = Mathf.Clamp(joystickY, -35, 60);

        Target.rotation = Quaternion.Euler(joystickY, joystickX, 0);
        Player.rotation = Quaternion.Euler(0, joystickX, 0);

        
        transform.LookAt(Target); //Camera focused on correct area
        
    }

}
