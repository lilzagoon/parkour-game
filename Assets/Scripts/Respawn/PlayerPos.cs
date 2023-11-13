using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    //Get accsees to the game master
    public GameMaster GM;

    void start()
    {
        //setting the verialbe stored in the game master to be the most resent check point
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = GM.lastCheckPointPos;
    }


    void OnTriggerEnter(Collider other)
    {
        // setting players xyz to the storted postion in the game master
        if (gameObject.tag == "Player")
        {
            transform.position = GM.lastCheckPointPos;
        }
    }
}
