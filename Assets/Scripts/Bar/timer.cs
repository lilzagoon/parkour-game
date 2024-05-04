using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
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
        if (timerSlider.value >= 3f)
        {
            if (Input.GetButtonDown("Dash"))
            {
                timerSlider.value = 0f;
            }
        }

        if (timerSlider.value <= 3f)
        {
            timerSlider.value += Time.deltaTime;
        }

    }
}
