using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankDetermine : MonoBehaviour
{
    public Timer timer;
    public EndScreen ES;
    public int Stime = 0;
    public int Atime = 0;
    public int Btime = 0;
    public int Ctime = 0;
    public int Dtime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.currentTime >= Dtime)
        {
            Debug.Log("D");
            ES.rankPoints = 500;
        }

        if (timer.currentTime >= Btime && timer.currentTime <= Ctime)
        {
            Debug.Log("C");
            ES.rankPoints = 1500;
        }

        if (timer.currentTime >= Atime && timer.currentTime <= Btime)
        {
            Debug.Log("B");
            ES.rankPoints = 2500;
        }

        if (timer.currentTime >= Stime && timer.currentTime <= Atime)
        {
            Debug.Log("A");
            ES.rankPoints = 3500;
        }

        if (timer.currentTime <= Stime)
        {
            Debug.Log("S");
            ES.rankPoints = 4500;
        }

    }
}
