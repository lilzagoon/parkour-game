using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public PlayerMovementTwo pm;
    private bool canSell = false;
    private Dashing _dashing;
    private GrappleGun _grappleGun;
    private Shoot _shoot;
    public float speedBonus;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovementTwo>();
        _dashing = player.GetComponent<Dashing>();
        _grappleGun = player.GetComponentInChildren<GrappleGun>();
        _shoot = player.GetComponentInChildren<Shoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canSell == true && Input.GetKeyDown(KeyCode.J))
        {
            MoveSpeedSale();
            canSell = false;
        }
        
        if (canSell == true && Input.GetKeyDown(KeyCode.K))
        {
            JumpUpgrade();
            canSell = false;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            //DashUpgrade();
            canSell = false;
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            //BombUpgrade();
            canSell = false;
        }
        
    }

    void MoveSpeedSale()
    {
        pm.coins-=3;
        pm.movementUpgrades++;
        Debug.Log("Movement Speed Sale made!");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canSell = true;
        }
    }
    

    private void JumpUpgrade()
    {
        pm.coins -= 3;
        Debug.Log("Upgraded Jump!");
        pm.jumpUpgrades++;
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canSell = false;
        }
    }
}
