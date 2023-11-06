using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZoomOut : MonoBehaviour
{
    public Vector3 playerSpeed;
    float normalFov = 60f;
    float zoomOutFov = 100f;
    float smooth = 1f;
    public PlayerMove playerMove;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMove = (GameObject.Find("Player").GetComponent<PlayerMove>());
        playerSpeed = (GameObject.Find("Player").GetComponent<PlayerMove>().rb.velocity);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Player_Speed: " + playerMove.rb.velocity.ToString("F2"));
        //Player speed is successfully accessed from another script.   
    }
}
