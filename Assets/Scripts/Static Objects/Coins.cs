using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    [ContextMenu("Generate guid for id")]
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public PlayerMovementTwo pm;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pm = player.GetComponent<PlayerMovementTwo>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadData(GameData data)
    {
        if(data.coinsList.Contains(this.id))
        {
            this.gameObject.SetActive(false);
        }
    }
    public void SaveData(ref GameData data)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        pm.coinsList.Add(this.id);
        pm.coins++;
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Collectable");
        this.gameObject.SetActive(false);
    }
}