using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{
    public Collider manCollider;

    public float Speed = 50f; //Character's move speed
    public float RunSpeed = 200f; //Character speed while running
    public float WalkSpeed = 50f; //Character speed while walking

    // Update is called once per frame

    void Start()
    {
    }

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float horizantalMovemeneInput = Input.GetAxis("Horizontal"); //movement input for horizontal
        float verticalMovemeneInput = Input.GetAxis("Vertical"); //movement input for vertical

        Vector3 playerMovement = new Vector3(horizantalMovemeneInput, 0f, verticalMovemeneInput) * Speed * Time.deltaTime; //allows character to move across terrain
        transform.Translate(playerMovement, Space.Self); //actually moves the character

        if(Input.GetKeyDown(KeyCode.LeftShift)) //If the left shift is held down
        {
            Speed = RunSpeed;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed = WalkSpeed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
        
    }

    
}
