using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GunModelSwap : MonoBehaviour
{
    public GameObject player;
    public PlayerMovementTwo pm;
    public Material gunColour1;
    public Material gunColour2;
    public Color32 color1;
    public Color32 color2;
    public int amountM = 1;
    public int amountJ = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovementTwo>();
        gunColour1 = Resources.Load<Material>("GunColour");
        gunColour2 = Resources.Load<Material>("GunColour2");
        color1 = new Color32(17, 255, 0, 255);
        color2 = new Color32(0, 70, 18, 255);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void Pink()
    {
        color1 = new Color32(206, 65, 126, 255);
        color2 = new Color32(87, 15, 46, 255);
        gunColour1.SetColor("_BaseColor", color1);
        gunColour2.SetColor("_BaseColor", color2);
    }

    public void Blue()
    {
        color1 = new Color32(74, 205, 255, 255);
        color2 = new Color32(21, 74, 94, 255);
        gunColour1.SetColor("_BaseColor", color1);
        gunColour2.SetColor("_BaseColor", color2);
    }

    public void Gold()
    {
        if (pm.goldUnlock == false && pm.coins >= 4)
        {
            pm.goldUnlock = true;
            pm.coins -= 4;
            color1 = new Color32(255, 226, 88, 255);
            color2 = new Color32(109, 90, 0, 255);
            gunColour1.SetColor("_BaseColor", color1);
            gunColour2.SetColor("_BaseColor", color2);
        }
        else if (pm.goldUnlock == true)
        {
            color1 = new Color32(255, 226, 88, 255);
            color2 = new Color32(109, 90, 0, 255);
            gunColour1.SetColor("_BaseColor", color1);
            gunColour2.SetColor("_BaseColor", color2);
        }
    }

    public void ResetColour()
    {
        color1 = new Color32(17, 255, 0, 255);
        color2 = new Color32(0, 70, 18, 255);
        gunColour1.SetColor("_BaseColor", color1);
        gunColour2.SetColor("_BaseColor", color2);
    }
    
    public void MoveSpeedSale()
    {
        if (amountM == 1)
        {
            if (pm.coins >= 3)
            {
                pm.coins -= 3;
                pm.movementUpgrades++;
                Debug.Log("Movement Speed Sale made!");
                amountM -= 1;
            }
        }
    }

    public void JumpUpgrade()
    {
        if (amountJ == 1)
        {
            if (pm.coins >= 3)
            {
                pm.coins -= 3;
                Debug.Log("Upgraded Jump!");
                pm.jumpUpgrades++;
                amountJ -= 1;
            }
        }
    }

    public void ExitShop()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene("Scenes/Hub World");
    }
}
