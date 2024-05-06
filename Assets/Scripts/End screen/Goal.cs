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
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovementTwo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Scene scene = SceneManager.GetActiveScene();
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Victory");
            endScreen.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;
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
        }
        
    }
}
