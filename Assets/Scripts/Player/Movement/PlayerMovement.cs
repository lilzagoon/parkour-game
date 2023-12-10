using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 3f;
    public float runSpeed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 1f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float fallSpeed;
    public bool isSprinting;

    public GameObject visualiser;
    
    float x, z;
    Vector3 velocity;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded)
        {
            velocity.y = 0f;
        }

        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
        }
        
        
        if (Input.GetKey("left shift"))
        {
            speed = runSpeed;
            isSprinting = true;
        }
        else
        {
            speed = 3f;
            isSprinting = false;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (velocity.y > -30f && !isGrounded )
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if (velocity.y < 20f)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        
        controller.Move(velocity * Time.deltaTime);

        fallSpeed = velocity.y;
        
    }
}

    