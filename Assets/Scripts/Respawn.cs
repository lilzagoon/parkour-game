using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

   // public GameObject spawnPoint;
    public GameObject player;
    public GameMaster GM;

    void start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.transform.tag == "Player")
        {
            player.transform.position = GM.lastCheckPointPos;
        }
    }
}
