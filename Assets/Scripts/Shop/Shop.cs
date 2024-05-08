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
        if (Input.GetButtonDown("Shop"))
        {
            SceneManager.LoadScene("Scenes/Shop");
        }
    }
}
