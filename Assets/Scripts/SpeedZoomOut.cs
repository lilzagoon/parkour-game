using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZoomOut : MonoBehaviour
{
    public float player_speed;
    public float last_speed;
    public float currentFov; //currentQuantity
    public float desiredFov; //desiredQuantity
    const float zoomStep = 7.0f;
    public ParticleSystem animeLines;
 
    void Start()
    {
        currentFov = 60f;
        desiredFov = currentFov;
    }
 
    void CheckSpeed()
    {
        if (player_speed < last_speed)
        {
            print("Player Speed Decreasing - Zoom IN!");
            last_speed = player_speed;
            desiredFov = 60;
            //currentFOV to minFOV
        }
        else if (player_speed > last_speed)
        {
            print("Player Speed Increasing - Zoom OUT!");
            last_speed = player_speed;
            desiredFov = 90f;
            //current FOV to maxFOV
        }  
    }
 
    void ProcessFOV()
    {
        currentFov = Mathf.MoveTowards(currentFov, desiredFov, zoomStep * Time.deltaTime);
    }
 
    void SetFOV()
    {
        Camera.main.fieldOfView = currentFov;
    }
    

    void Update()
    {
        player_speed = (GameObject.Find("Player").GetComponent<PlayerMove>().rb.velocity.magnitude);
 
        CheckSpeed();
        ProcessFOV();
        SetFOV();

        if (player_speed >= 45f)
        {
            animeLines.Play();
        }
        else
        {
            animeLines.Stop();
        }
    }
}
