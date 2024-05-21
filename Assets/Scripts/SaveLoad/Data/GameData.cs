using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<string> coinsList;
    public int coins;
    public int grappleUpgrades;
    public int dashUpgrades;
    public int bombUpgrades;
    public int jumpUpgrades;
    public int moveUpgrades;
    public bool jumpUpgraded;
    public bool moveUpgraded;
    public bool pinkUnlock;
    public bool blueUnlock;
    public bool yellowUnlock;
    
    public GameData()
    {
        this.coinsList = new List<string>();
    }
}
