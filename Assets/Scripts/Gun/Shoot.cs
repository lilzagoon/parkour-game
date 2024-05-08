using UnityEngine;
using System.Collections;
     
public class Shoot : MonoBehaviour
{
    public Rigidbody projectile;
    public float speed = 200;
    public float bombCd;
    private float maxCd;
    private float bombCdTimer;
    private RocketGun _rocketGun;
    private bool reloaded;

    void Start()
    {
        reloaded = false;
        maxCd = bombCd;
        bombCdTimer = maxCd;
        _rocketGun = GameObject.Find("Plunger").GetComponent<RocketGun>();
    }

    public void Recalculate (int upgrades)
    {
        maxCd = bombCd - upgrades;
    }

    void Update ()
    {
        float fireAxis = Input.GetAxisRaw("Fire2");
        bool fire = Input.GetButtonDown("Fire2") || (fireAxis > 0);
        
        if (bombCdTimer > 0)
            bombCdTimer -= Time.deltaTime;

        if (bombCdTimer <= 0 && reloaded == false)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Reload");
            reloaded = true;
        }
        
        if ((fire) && bombCdTimer <= 0)
        {
            _rocketGun.anim.SetTrigger("mouse2");
            Rigidbody instantiatedProjectile = Instantiate(projectile,transform.position,transform.rotation)as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0,speed));
            bombCdTimer = maxCd;
            reloaded = false;
        }
    }
}