using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    public float RotationSpeed = 1; //the speed of rotation
    public Transform Target, Player;
    public float mouseX, mouseY;

    void Start()
    {
        Cursor.visible = false; //Removes cursor
        Cursor.lockState = CursorLockMode.Locked; //Locks cursor to center of screen
    }

    void LateUpdate()
    {
        CamControl();
    }

    void CamControl() //Controls the camera
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed; //Controls camera moving left and right
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed; //Controls camera moving up and down
        mouseY = Mathf.Clamp(mouseY, -35, 60); //Prevent cam from flipping all the way aroud the player up and down

        transform.LookAt(Target); //Camera focused on correct area

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);//Allows cam to rotate
        Player.rotation = Quaternion.Euler(0, mouseX, 0); //Allows player to rotate

        
    }
    
}
