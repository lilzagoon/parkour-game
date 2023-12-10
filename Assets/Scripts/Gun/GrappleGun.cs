using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleGun : MonoBehaviour
{
    public GameObject plungerModel;
    private LineRenderer _lineRenderer;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrapplable;
    public Transform gunTip, camera, player;
    public float maxDistance = 30000f;
    private SpringJoint joint;
    public AudioSource plungerSound;
    public AudioClip plungerClip;
    public AudioClip GrappleClip;
    public Animator rocketAnim;
    private bool grappleHit;
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        plungerSound = GetComponent<AudioSource>();
        grappleHit = false;
        plungerModel.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }
    
    void StartGrapple()
    {
        rocketAnim.enabled = false;
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrapplable))
        {
            plungerModel.SetActive(false);
            plungerSound.PlayOneShot(GrappleClip);
            grappleHit = true;
            grapplePoint = hit.transform.position;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.0f;
            joint.minDistance = distanceFromPoint * 0.4f;
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            _lineRenderer.positionCount = 2;
            
            
            
        }
    }

    void StopGrapple()
    {
        rocketAnim.enabled = true;
        _lineRenderer.positionCount = 0;
        Destroy(joint);
        if (grappleHit == true)
        {
            plungerSound.PlayOneShot(plungerClip);
            plungerModel.SetActive(true);
            grappleHit = false;
        }
    }

    void DrawRope()
    {
        if (!joint) return;
        
        _lineRenderer.SetPosition(0, gunTip.position);
        _lineRenderer.SetPosition(1, grapplePoint);
    }

    public bool IsGrappling()
    {
        
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}