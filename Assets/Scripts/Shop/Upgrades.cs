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
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovementTwo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canSell == true && Input.GetKeyDown(KeyCode.J))
        {
            Sale();
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canSell = false;
        }
    }
}
