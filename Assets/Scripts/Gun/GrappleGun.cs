using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class GrappleGun : MonoBehaviour
{
    public PlayerMovementTwo pm;
    public GameObject plungerModel;
    private LineRenderer _lineRenderer;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrapplable;
    public LayerMask whatIsNPC;
    public Transform gunTip, camera, player;
    public float maxDistance = 30000f;
    private SpringJoint joint;
    public AudioSource plungerSound;
    public AudioClip plungerClip;
    public AudioClip GrappleClip;
    public Animator rocketAnim;
    private bool grappleHit;
    public DialogueTrigger dialogueTrigger;
    private bool lookingAtNPC;
    public float grappleCd;
    private float grappleCdTimer;
    private bool isGrappling = false;


    [Header("Air Movement")] public Transform orientation;
    public Rigidbody rb;
    public float horizontalThrustForce;
    public float forwardThrustForce;
    public float extendCableSpeed;

    [Header("Prediction")] public RaycastHit predictionHit;
    public float predictionSphereCastRadius;
    public Transform predictionPoint;
    private bool cursorLook;


    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        plungerSound = GetComponent<AudioSource>();
        grappleHit = false;
        plungerModel.SetActive(true);
        pm = player.GetComponent<PlayerMovementTwo>();
        cursorLook = false;
        lookingAtNPC = false;
        grappleCd = 0.3f;
        grappleCdTimer = grappleCd;
        DontDestroyOnLoad(predictionPoint);
    }

    public void Recalculate (int upgrades)
    {
        maxDistance = 125 + (10 * upgrades);
        forwardThrustForce = 2000 + (500 * upgrades);
    }

    void Update()
    {
        if (grappleCdTimer > 0)
            grappleCdTimer -= Time.deltaTime;
        
        if (Input.GetButtonDown("Fire1") && !isGrappling)
        {
            StartGrapple();
        }

        else if (Input.GetButtonUp("Fire1"))
        {
            StopGrapple();
            grappleCdTimer = grappleCd;
        }

        if (Input.GetKeyDown(KeyCode.R) && lookingAtNPC == true)
        {
            dialogueTrigger.TriggerDialogue();
        }

        if (joint != null) AirMovement();

        CheckForSwingPoints();
        CursorCheck();
        //dialogueCheck();
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        isGrappling = true;
        predictionPoint.gameObject.SetActive(false);
        rocketAnim.enabled = false;

        if (predictionHit.point == Vector3.zero) return;
        
        pm.swinging = true;
        plungerModel.SetActive(false);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Rope");
        grappleHit = true;
        grapplePoint = predictionHit.transform.position;
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grapplePoint;

        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;

        _lineRenderer.positionCount = 2;
    }

    void StopGrapple()
    {
        isGrappling = false;
        pm.swinging = false;
        rocketAnim.enabled = true;
        _lineRenderer.positionCount = 0;
        Destroy(joint);
        if (grappleHit == true)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Suction Cup");
            plungerModel.SetActive(true);
            grappleHit = false;
        }
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        _lineRenderer.SetPosition(0, gunTip.position);
        _lineRenderer.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {

        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    void AirMovement()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        
        //what the fuck does this do
        if (Input.GetKey(KeyCode.D)) rb.AddForce(orientation.right * horizontalThrustForce * Time.deltaTime);
        if (Input.GetKey(KeyCode.A)) rb.AddForce(-orientation.right * horizontalThrustForce * Time.deltaTime);
        if (Input.GetKey(KeyCode.W)) rb.AddForce(orientation.forward * forwardThrustForce * Time.deltaTime);
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = grapplePoint - transform.position;
            rb.AddForce(directionToPoint.normalized * forwardThrustForce * Time.deltaTime);

            float distanceFromPoint = Vector3.Distance(transform.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.0f;
            joint.minDistance = distanceFromPoint * 0.4f;
        }
        
        //why ISN'T IT UP THERE WITH THE REST
        if (Input.GetKey(KeyCode.S))
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, grapplePoint) + extendCableSpeed;

            joint.maxDistance = extendedDistanceFromPoint * 0.0f;
            joint.minDistance = extendedDistanceFromPoint * 0.4f;
        }
    }

    void CheckForSwingPoints()
    {
        if (joint != null) return;

        RaycastHit sphereCastHit;
        Physics.SphereCast(camera.position, predictionSphereCastRadius, camera.forward, out sphereCastHit, maxDistance,
            whatIsGrapplable);

        RaycastHit raycastHit;
        Physics.Raycast(camera.position, camera.forward, out raycastHit, maxDistance, whatIsGrapplable);

        Vector3 realHitPoint;

        //Check 1 - Direct Hit
        if (raycastHit.point != Vector3.zero)
            realHitPoint = raycastHit.point;

        //Check 2 - Predicted Hit
        else if (sphereCastHit.point != Vector3.zero)
            realHitPoint = sphereCastHit.point;

        //Check 3 - No Hit
        else realHitPoint = Vector3.zero;


        if (realHitPoint != Vector3.zero && joint == null)
        {
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.position = realHitPoint;
        }

        else
        {
            predictionPoint.gameObject.SetActive(false);
        }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
    }

    void CursorCheck()
    {
        RaycastHit cursorHit;
        if (Physics.Raycast(camera.position, camera.forward, out cursorHit, maxDistance, whatIsGrapplable))
        {
            cursorLook = true;
        }
        else
        {
            cursorLook = false;
        }

        // if (cursorLook == true)
        // {
        //     cursorTransform.eulerAngles = new Vector3(0, 0, 45);
        // }
        // else
        // {
        //     cursorTransform.eulerAngles = new Vector3(0, 0, 0);
        // }
    }
}

/*void dialogueCheck()
{

    RaycastHit npcHit;
    if (Physics.Raycast(camera.position, camera.forward, out npcHit, 10, whatIsNPC))
    {
        lookingAtNPC = true;
        cursorTransform.eulerAngles = new Vector3(0, 0, 45);
    }
    else
    {
        lookingAtNPC = false;
        cursorTransform.eulerAngles = new Vector3(0, 0, 0);
    }
}
}*/