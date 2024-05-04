using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneCamera : MonoBehaviour
{

    public GameObject startCamera;
    public GameObject C2Camera;

    // Start is called before the first frame update
    void Start()
    {
        startCamera.SetActive(true);
        C2Camera.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerEnter()
    {
        startCamera.SetActive(false);
        C2Camera.SetActive(true);

        print("Button Pressed baybeeee");
    }
}
