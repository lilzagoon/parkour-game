using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public EndScreen endScreen;
    public GameObject player;
    public PlayerMovementTwo pm;
    private Timer timerScript;
    public bool justRestarted = true;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovementTwo>();
        timerScript = GameObject.Find("UI").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerScript.currentTime >= 1)
        {
            justRestarted = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && justRestarted == false)
        {
            Scene scene = SceneManager.GetActiveScene();
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Victory");
            endScreen.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;
            justRestarted = true;
            if (scene.name == "GP L3")
            {
                pm.GrappleUpgrade();
            }
            if (scene.name == "RL L3")
            {
                pm.BombUpgrade();
            }
            if (scene.name == "CS L3")
            {
                pm.DashUpgrade();
            }
            DataPersistenceManager.instance.SaveGame();
        }
        
    }
}
