using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentCoins : MonoBehaviour
{
    public GameObject player;
    private PlayerMovementTwo pm;
    public TextMeshProUGUI coinsText;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovementTwo>();
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = "CURRENT BRUSHES: " + pm.coins;
    }
}
