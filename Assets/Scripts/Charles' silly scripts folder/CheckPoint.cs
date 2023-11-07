using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //get ref to game master
    public GameMaster GM;

        void start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void OnTriggerEnter(Collider other)
    {
        //setting the check points postion the be stored in the game master
        if (other.gameObject.tag == "Player")
        {
            GM.lastCheckPointPos = transform.position;

            GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
    }
}
