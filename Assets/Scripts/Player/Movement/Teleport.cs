using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterController : MonoBehaviour
{
    public bool active = false;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform playerCam;
    [SerializeField] private Rigidbody playerBody;
    // Start is called before the first frame update
    void Start()
    {
        rb.isKinematic = true;
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        rb.isKinematic = true;
        gameObject.SetActive(false);
    }

    public void Launch()
    {
        rb.isKinematic = false;
        transform.position = playerCam.position + (playerCam.forward * 0.2f);
        rb.velocity = (playerCam.forward * speed) + (playerBody.velocity / 2);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rb.isKinematic = true;
        }
    }
}