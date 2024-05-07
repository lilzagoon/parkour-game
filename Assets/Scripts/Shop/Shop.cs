using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    public GameObject shopUI;
    public static bool shopMenuOpen = false;
    public GameObject player;
    public PlayerMovementTwo pm;
    public int amountM = 1;
    public int amountJ = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovementTwo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Scenes/Shop");
        }
    }

    public void Open()
    {
        shopUI.SetActive(true);
        Time.timeScale = 0f;
        shopMenuOpen = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void Close()
    {
        shopUI.SetActive(false);
        Time.timeScale = 1f;
        shopMenuOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    

}
