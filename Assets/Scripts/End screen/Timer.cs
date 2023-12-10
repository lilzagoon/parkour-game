using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float targetTime;
    public float currentTime;
    public float roundedTime;
    public TextMeshProUGUI timerText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        double roundedTime=System.Math.Round(currentTime,2);
        timerText.text = (roundedTime).ToString("");
    }
}
