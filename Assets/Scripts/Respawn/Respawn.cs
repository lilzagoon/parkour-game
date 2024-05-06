using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

   // public GameObject spawnPoint;
    public GameObject player;
    public GameMaster GM;

    void Start()
    {
       // GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.transform.tag == "Player")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Death");
            player.transform.position = GM.lastCheckPointPos;   
        }
    }
}
