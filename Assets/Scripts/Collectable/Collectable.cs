using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public GameObject player;
    public PlayerMovementTwo collect;
    public TextMeshProUGUI count;
    public int show;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        collect = player.GetComponent<PlayerMovementTwo>();
    }

    // Update is called once per frame
    void Update()
    {
        show = collect.coins;
        count.text = show.ToString();
    }
}
