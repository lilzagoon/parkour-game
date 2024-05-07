using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheMuter : MonoBehaviour
{



    private Music muse;


   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Mute()
    {
        muse.Mute();
    }
}
