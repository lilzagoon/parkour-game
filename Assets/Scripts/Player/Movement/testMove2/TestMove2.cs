using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove2 : MonoBehaviour
{
    public float moveSpeed;

    public Transform orientation;

    private float horizontalInput;

    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
}
