using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform camHolder;
    private PlayerMovementTwo pm;
    public GameObject player;
    
    private float xRotation;
    private float yRotation;
    public float regularFov;
    public float speedFov;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovementTwo>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensY;
        float joystickX = Input.GetAxisRaw("HorizontalJ") * Time.fixedDeltaTime * sensX;
        float joystickY = Input.GetAxisRaw("VerticalJ") * Time.fixedDeltaTime * sensY;

        if (mouseX != 0 || mouseY != 0)
        {
            yRotation += mouseX;
            xRotation -= mouseY;   
        }
        else
        {
            yRotation += joystickX;
            xRotation -= joystickY;
        }
        
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        // DoSprintFov();
    }

    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.5f);
    }

    public void DoSprintFov()
    {
        if (pm.sprinting) GetComponent<Camera>().DOFieldOfView(speedFov, 2f);
        else GetComponent<Camera>().DOFieldOfView(regularFov, 1f);
    }
    
    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.5f);
    }
}
