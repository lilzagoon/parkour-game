using UnityEngine;
using System.Collections;
     
public class Shoot : MonoBehaviour
{
    public Rigidbody projectile;
    public float speed = 200;
    public float bombCd;
    private float bombCdTimer;
    private RocketGun _rocketGun;

    void Start()
    {
        bombCdTimer = bombCd;
        _rocketGun = GameObject.Find("Plunger").GetComponent<RocketGun>();
    }
    void Update ()
    {
        if (bombCdTimer > 0)
            bombCdTimer -= Time.deltaTime;
        
        if (Input.GetButtonDown("Fire2") && bombCdTimer <= 0)
        {
            _rocketGun.anim.SetTrigger("mouse2");
            Rigidbody instantiatedProjectile = Instantiate(projectile,transform.position,transform.rotation)as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0,speed));
            bombCdTimer = bombCd;
        }
    }
}