using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public int sceneNum;
    private GameObject player;
    private Dashing _dashing;
    private GrappleGun _grappleGun;
    private Shoot _shoot;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _dashing = player.GetComponent<Dashing>();
        _grappleGun = player.GetComponentInChildren<GrappleGun>();
        _shoot = player.GetComponentInChildren<Shoot>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (sceneNum == 3)
            {
                _dashing.enabled = false;
                _shoot.enabled = false;
            }

            if (sceneNum == 6)
            {
                _grappleGun.enabled = false;
                _dashing.enabled = false;
            }
            
            if (sceneNum == 9)
            {
                _grappleGun.enabled = false;
                _shoot.enabled = false;
            }
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            SceneManager.LoadScene(sceneNum);
        }
    }
}
