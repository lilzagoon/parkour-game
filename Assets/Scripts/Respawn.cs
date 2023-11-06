using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{

    public GameObject spawnPoint;
    public GameObject player;

    void OnTriggerEnter (Collider other)
    {
        if (other.transform.tag == "Player")
        {
            player.transform.position = spawnPoint.transform.position;
        }
    }
}
