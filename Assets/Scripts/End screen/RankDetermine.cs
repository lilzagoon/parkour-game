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
        if(timer.roundedTime <= 55)
        {
            Debug.Log("S");
            ES.rankPoints = 4500;
        }

        if (timer.roundedTime <= 65 && timer.roundedTime >= 55)
        {
            Debug.Log("A");
            ES.rankPoints = 3500;
        }

        if (timer.roundedTime <= 75 && timer.roundedTime >= 65)
        {
            Debug.Log("B");
            ES.rankPoints = 2500;
        }

        if (timer.roundedTime <= 85 && timer.roundedTime >= 75)
        {
            Debug.Log("C");
            ES.rankPoints = 1500;
        }

        if (timer.roundedTime >= 85)
        {
            Debug.Log("D");
            ES.rankPoints = 500;
        }
    }
}
