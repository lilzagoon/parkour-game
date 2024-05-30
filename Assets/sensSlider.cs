using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class sensSlider : MonoBehaviour
{
    public PlayerCam _playerCam;
    public GameObject player;
    public Slider slider;
    public TMP_Text sensText;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        _playerCam = player.GetComponentInChildren<PlayerCam>();
        sensText.text = Mathf.Round(slider.value).ToString();
    }

    // Update is called once per frame


    public void MouseSensSlider()
    {
        _playerCam.sensX = slider.value;
        _playerCam.sensY = slider.value;
        sensText.text = Mathf.Round(slider.value).ToString();
    }
    
    public void ControllerSensSlider()
    {
        _playerCam.controllerSensX = slider.value;
        _playerCam.controllerSensY = slider.value;
        sensText.text = Mathf.Round(slider.value).ToString();
    }
}
