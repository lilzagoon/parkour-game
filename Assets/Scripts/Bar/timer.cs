using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public GameObject slider;
    public Slider timerSlider;
    public GameObject player;
    public PlayerMovementTwo pm;
    public Dashing dash;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovementTwo>();
        dash = player.GetComponent<Dashing>();
        timerSlider.maxValue = dash.dashCd;
        timerSlider.value = dash.dashCd;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerSlider.value >= timerSlider.maxValue)
        {
            slider.SetActive(false);
            if (Input.GetButtonDown("Dash"))
            {
                slider.SetActive(true);
                timerSlider.value = 0f;
            }
        }

        if (timerSlider.value <= timerSlider.maxValue && pm.grounded)
        {
            timerSlider.value += Time.deltaTime;
        } else if (timerSlider.value <= timerSlider.maxValue)
            timerSlider.value += Time.deltaTime / 5;
        
    }
}
