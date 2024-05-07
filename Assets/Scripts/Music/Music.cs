using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    private FMOD.Studio.EventInstance instance;

    
    public FMODUnity.EventReference fmodEvent;

    public int sceneNum;
   
    void Start()
    {
        
        sceneNum = SceneManager.GetActiveScene().buildIndex;
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
        UnMute();
        instance.start();
        instance.release();
    }

   
    void Update()
    {
        sceneNum = SceneManager.GetActiveScene().buildIndex;
        instance.setParameterByName("Level", sceneNum);
        


    }

    public void Mute()
    {
        instance.setParameterByName("On or Off", 1);
    }

    public void UnMute()
    {
        instance.setParameterByName("On or Off", 0);
    }
}
