using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            DestroyObject(player);
            Debug.Log("destryoed");
        }

        else
        {
            Debug.Log("No player found in scene.");
        }
    }
}
