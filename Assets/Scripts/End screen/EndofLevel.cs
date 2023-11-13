using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndofLevel : MonoBehaviour
{
  public void Restart()
    {
        SceneManager.LoadScene("Whitebox");
    }

    public void Quite()
    {
        Application.Quit();
    }
}
