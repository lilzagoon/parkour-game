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

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sceneNum = SceneManager.GetActiveScene().buildIndex;
    }

    public void Next()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNum +1);
        
    }


  public void Restart()
    {
        Time.timeScale = 1;
        Destroy(player);
        SceneManager.LoadScene(sceneNum);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }
}
