using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{

    public PlayerMovementTwo pm;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pm = player.GetComponent<PlayerMovementTwo>();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        pm.coinsList.Add(this.gameObject);
        pm.coins++;
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Collectable");
        this.gameObject.SetActive(false);
    }
}
