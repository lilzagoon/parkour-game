using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndofLevel : MonoBehaviour
{
    public int sceneNum;
    public GameObject player;
    private Dashing _dashing;
    private GrappleGun _grappleGun;
    private Shoot _shoot;
    private PlayerMovementTwo pm;
    public GameObject gm;
    public GameObject goal;
    public Goal goalScript;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sceneNum = SceneManager.GetActiveScene().buildIndex;
        _dashing = player.GetComponent<Dashing>();
        _grappleGun = player.GetComponentInChildren<GrappleGun>();
        _shoot = player.GetComponentInChildren<Shoot>();
        gm = GameObject.FindGameObjectWithTag("GM");
        goal = GameObject.Find("Goal");
        goalScript = goal.GetComponent<Goal>();
    }

    public void Next()
    {
        Time.timeScale = 1;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        SceneManager.LoadScene(sceneNum +1);
    }


  public void Restart()
  {
        player.transform.position = gm.transform.position;
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNum);
  }

    public void Menu()
    {
        Time.timeScale = 1;
        _shoot.enabled = true;
        _dashing.enabled = true;
        _grappleGun.enabled = true;
        SceneManager.LoadScene(2);
    }
}
