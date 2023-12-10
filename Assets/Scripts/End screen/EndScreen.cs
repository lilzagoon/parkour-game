using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public TextMeshProUGUI endTime;
    public TextMeshProUGUI rankText;

    public Timer timerScript;

    public string rank;

    public int rankPoints;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        endTime.text = "Timer: " + timerScript.currentTime;
      
        if (rankPoints <= 1000 && rankPoints >= 0)
        {
            rank = "D";
        }
        
        if (rankPoints <= 2000 && rankPoints >= 1001)
        {
            rank = "C";
        }
        
        if (rankPoints <= 3000 && rankPoints >= 2001)
        {
            rank = "B";
        }
        
        if (rankPoints <= 4000 && rankPoints >= 3001)
        {
            rank = "A";
        }
        
        if (rankPoints <= 5000 && rankPoints >= 4001)
        {
            rank = "S";
        }
        
        rankText.text = "Rank: " + rank;
    }

    void Rank()
    {
    }
}
