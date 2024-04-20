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
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovementTwo>();
        _dashing = player.GetComponent<Dashing>();
        _grappleGun = player.GetComponentInChildren<GrappleGun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canSell == true && Input.GetKeyDown(KeyCode.J))
        {
            Sale();
            canSell = false;
        }

        if (canSell == true && Input.GetKeyDown(KeyCode.O))
        {
            DashUpgrade();
            canSell = false;
        }

        if (canSell == true && Input.GetKeyDown(KeyCode.P))
        {
            BombUpgrade();
            canSell = false;
        }

        if (canSell == true && Input.GetKeyDown(KeyCode.L))
        {
            GrappleUpgrade();
            canSell = false;
        }
    }

    void Sale()
    {
        pm.coins-=3;
        Debug.Log("Sale made!");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canSell = true;
        }
    }

    private void DashUpgrade()
    {
        pm.coins -= 3;
        Debug.Log("Upgraded Dash!");
        _dashing.dashDuration += 20;
    }

    void BombUpgrade()
    {
        pm.coins -= 3;
        Debug.Log("Upgraded Bomb!");
    }

    void GrappleUpgrade()
    {
        pm.coins -= 3;
        _grappleGun.maxDistance += 10f;
        _grappleGun.forwardThrustForce += 10f;
        Debug.Log("Upgraded Grapple!");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canSell = false;
        }
    }
}
