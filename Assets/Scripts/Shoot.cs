using UnityEngine;
using System.Collections;
     
public class Shoot : MonoBehaviour
{
    public Rigidbody projectile;
    public float speed = 200;
     
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Rigidbody instantiatedProjectile = Instantiate(projectile,transform.position,transform.rotation)as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0,speed));
        }
    }
}