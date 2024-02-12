using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMovement : MonoBehaviour
{
    public float runSpeed;
    public float mouseSensitivity;

    private float hInput, vInput;
    private float pitch, yaw;

    private int groundContact = 0;

    private bool fireHeld = false;
    private bool teleActive = false;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cam;
    [SerializeField] private Camera camInfo;

    [SerializeField] private LayerMask groundMask;

    [SerializeField] private TeleporterController teleporter;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        Vector3 vel = (transform.forward * vInput) + (transform.right * hInput);
        if (vel.magnitude > 1) vel = vel.normalized;
        rb.velocity = (vel * runSpeed) + new Vector3(0, rb.velocity.y, 0);

        if(groundContact > 0)
        {
            if(Input.GetAxis("Jump") > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, 10, rb.velocity.z);
            }
        }

    }

    void Update()
    {
        pitch = Mathf.Clamp(pitch - (Input.GetAxis("Mouse Y") * mouseSensitivity), -90, 90);
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        cam.rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.rotation = Quaternion.Euler(0, yaw, 0);

        float targetFOV = 75 + rb.velocity.magnitude / 3;
        if (camInfo.fieldOfView > 100)
        {
            camInfo.fieldOfView = 100;
        }
        if (camInfo.fieldOfView < 75) camInfo.fieldOfView = 75;
        else if (camInfo.fieldOfView < targetFOV) camInfo.fieldOfView += Time.deltaTime * 20;
        else if (camInfo.fieldOfView > targetFOV) camInfo.fieldOfView -= Time.deltaTime * 100;

        if (Input.GetAxis("Fire1") > 0)
        {
            if (!fireHeld)
            {
                if (!teleActive)
                {
                    teleporter.gameObject.SetActive(true);
                    teleporter.Launch();
                }
                else
                {
                    transform.position = teleporter.transform.position;
                    teleporter.Reset();
                }
                teleActive = !teleActive;
            }
            fireHeld = true;
        }
        else
        {
            fireHeld = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundContact++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            groundContact--;
        }
    }
}