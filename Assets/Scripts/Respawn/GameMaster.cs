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
    bool spawnpointRan = false;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovementTwo>();
        pm.groundContact = 0;
    }

    private void Update()
    {
        if (!spawnpointRan)
        {
            Cursor.visible = false;
            player.transform.position = this.transform.position;
            spawnpointRan = true;
            Debug.Log("hey ur being tped");
        }
    }
}
