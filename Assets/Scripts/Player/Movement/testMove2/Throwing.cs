using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Throwing : MonoBehaviour
{

    [Header("References")] 
    public Transform cam;
    public Transform throwPoint;
    public GameObject objectToThrow;
    private PlayerMovementTwo pm;
    private Rigidbody playerRb;
    
    [Header("Settings")] 
    public int totalThrows;
    public float throwCooldown;

    [Header("Throwing")] 
    public KeyCode throwKey = KeyCode.E;
    public float throwForce;
    public float throwUpwardForce;

    public bool readyToThrow;
    
    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovementTwo>();
        playerRb = GetComponent<Rigidbody>();
        readyToThrow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0)
        {
            Throw();
        }
    }

    void Throw()
    {
        readyToThrow = false;

        //spawning object
        GameObject projectile = Instantiate(objectToThrow, throwPoint.position, cam.rotation);

        //getting rigidbody
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        //calculating direction
//        Vector3 forceDirection = cam.transform.forward;
//
//        RaycastHit hit;
//
//        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
//{
//            forceDirection = (hit.point - throwPoint.position.normalized);
//        }
        
        //calculating force & adding it
        projectileRb.velocity = (cam.forward * pm.moveSpeed) + (playerRb.velocity / 2);
        Vector3 forceToAdd = cam.transform.forward * throwForce + transform.up * throwUpwardForce;
        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;
    }

    public void ResetThrow()
    {
        readyToThrow = true;
    }
}
