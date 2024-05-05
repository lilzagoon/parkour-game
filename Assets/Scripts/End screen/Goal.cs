using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public EndScreen endScreen;
    private PlayerMovementTwo pm;
    private GameObject player;
    
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

    public void OnTriggerEnter(Collider other)
    {
        Scene scene = SceneManager.GetActiveScene();
        
        if (other.tag == "Player")
        {
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
            
            endScreen.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }
}
