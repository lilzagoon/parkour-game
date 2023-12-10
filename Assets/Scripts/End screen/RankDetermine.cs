using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankDetermine : MonoBehaviour
{
    public Timer timer;
    public EndScreen ES;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.currentTime >= 85)
        {
            Debug.Log("D");
            ES.rankPoints = 500;
        }

        if (timer.currentTime >= 75 && timer.currentTime <= 85)
        {
            Debug.Log("C");
            ES.rankPoints = 1500;
        }

        if (timer.currentTime >= 65 && timer.currentTime <= 75)
        {
            Debug.Log("B");
            ES.rankPoints = 2500;
        }

        if (timer.currentTime >= 55 && timer.currentTime <= 65)
        {
            Debug.Log("A");
            ES.rankPoints = 3500;
        }

        if (timer.currentTime <= 55)
        {
            Debug.Log("S");
            ES.rankPoints = 4500;
        }

    }
}
