using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject settingsScreen;
    
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Setting()
    {
        //startScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }

    public void back()
    {
        startScreen.SetActive(true);
        settingsScreen.SetActive(false);
    }

    public void Quite()
    {
        Application.Quit();
    }
}
