using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAddon : MonoBehaviour
{

    private Rigidbody rb;
    private bool targetHit;
    private GameObject player;
    private Throwing throwing;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        throwing = player.GetComponent<Throwing>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (targetHit)
            return;
        else targetHit = true;

        rb.isKinematic = true;
        
        transform.SetParent(collision.transform);
        
        Teleport();
        
    }

    void Teleport()
    {
        player.transform.position = this.transform.position;
        throwing.readyToThrow = true;
        Destroy(this.gameObject);
    }
}
