using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementSceneManager : MonoBehaviour
{
    private GameObject player;
    private PlayerMovementTwo pm;
    private Scene scene;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovementTwo>();
        scene = SceneManager.GetActiveScene();
    }
    
    void Update()
    {
        if (scene.name == "Shop")
        {
            pm.enabled = false;
        }
        else
        {
            pm.enabled = true;
        }
    }
}
