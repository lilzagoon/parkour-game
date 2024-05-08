using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

   // public GameObject spawnPoint;
    public GameObject player;
    private PlayerMovementTwo pm;
    public GameMaster GM;

    void Start()
    {
       // GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovementTwo>();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.transform.tag == "Player")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Death");
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            pm.dashing = false;
            player.transform.position = GM.lastCheckPointPos;   
        }
    }
}
