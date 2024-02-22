using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
    public Vector3 lastCheckPointPos;
    public GameObject player;
    public PlayerMovementTwo pm;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = this.transform.position;
        pm = player.GetComponent<PlayerMovementTwo>();
        pm.groundContact = 0;
    }
}
