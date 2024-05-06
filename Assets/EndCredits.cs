using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            getOut();
        }
    }

    public void getOut()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
