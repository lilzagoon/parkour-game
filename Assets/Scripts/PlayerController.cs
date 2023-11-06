using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    private bool isGrounded;
    private Vector3 wallPoint;

    private float wallrunSpeed;
    private Vector3 prevSpeed;

    [SerializeField] private CapsuleCollider capsule;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private Transform camPivot;
    [SerializeField] private int camSens = 1;
    [SerializeField] private int jumpHeight = 1;
    [SerializeField] private int groundSpeed = 10;
    [SerializeField] private float airControl = 0.5f;
    [SerializeField] private float airMax = 5;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CameraControls();

        isGrounded = GroundCheck();

        if (isGrounded) GroundedActions();
        else AirActions();

        prevSpeed = rb.velocity;
    }

    void CameraControls()
    {
        mouseX += Input.GetAxis("Mouse X") * camSens;
        mouseY -= Input.GetAxis("Mouse Y") * camSens;

        transform.rotation = Quaternion.Euler(0, mouseX, 0);
        camPivot.localRotation = Quaternion.Euler(mouseY, 0, 0);
    }

    void GroundedActions()
    {
        if (Input.GetAxis("Jump") > 0)
        {
            Jump(jumpHeight);
        }
        else 
        {
            GroundMovement();
        }
    }
    void GroundMovement()
    {
        float xInp = Input.GetAxis("Horizontal");
        float zInp = Input.GetAxis("Vertical");

        Vector3 a = transform.forward * zInp;
        Vector3 b = transform.right * xInp;

        rb.velocity = Vector3.ClampMagnitude(a + b, 1) * groundSpeed * Time.deltaTime * 100;
    }

    void AirActions()
    {
        if (wallPoint != Vector3.zero)
        {
            WallActions();
        }
        else
        {
            AirMovement();
        }
    }
    void AirMovement()
    {
        Vector3 hVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        rb.AddForce(-hVel.normalized * airControl / 2);
        float xInp = Input.GetAxis("Horizontal");
        float zInp = Input.GetAxis("Vertical");

        Vector3 a = transform.forward * zInp;
        Vector3 b = transform.right * xInp;

        rb.AddForce(Vector3.ClampMagnitude(a + b, 1) * airControl);
        rb.velocity = Vector3.ClampMagnitude(new Vector3(rb.velocity.x, 0, rb.velocity.z), airMax) + new Vector3(0, rb.velocity.y, 0);
    }

    void WallActions()
    {
        WallMovement();
    }
    void WallMovement()
    {

        rb.AddForce(wallPoint - transform.position);
        Vector3 wallMove = (Quaternion.AngleAxis(90, Vector3.up) * (wallPoint - transform.position) * wallrunSpeed).normalized * groundSpeed * 0.5f;
        rb.velocity = new Vector3(wallMove.x, 0, wallMove.z);
    }

    void Jump(float jump)
    {
        rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(jump * Physics.gravity.y * -2), rb.velocity.z);
    }

    private bool GroundCheck()
    {
        float radius = capsule.radius * 0.95f;
        return(Physics.CheckSphere(transform.position + Vector3.up * -0.61f, radius, groundLayers));
    }

    void SetWallrunSpeed()
    {
        Vector3 diff = wallPoint - transform.position;
        float a = Mathf.Atan(diff.z / diff.x);
        wallrunSpeed = transform.forward.y - a;
        Debug.Log(wallrunSpeed);
    }

    void OnCollisionStay(Collision collision)
    {
        if (!isGrounded && collision.collider.tag == "Wallrun") 
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                if(contact.point.y <= transform.position.y) wallPoint = new Vector3(contact.point.x, transform.position.y, contact.point.z);
            }
            if(wallrunSpeed == 0) SetWallrunSpeed();
        }
        else wallPoint = Vector3.zero;
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Wallrun") 
        {
            wallPoint = Vector3.zero;
            wallrunSpeed = 0;
        }
    }
}
