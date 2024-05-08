using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{

    [Header("References")] 
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerMovementTwo pm;

    [Header("Dashing")] 
    public float dashForce;
    public float dashUpwardForce;
    public float maxDashYSpeed;
    public float dashDuration;
    private float finalDuration;

    [Header("Dashing")] 
    public PlayerCam cam;
    public float dashFov;
    [Header("Settings")] 
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = true;
    public bool resetVel = true;
    
    [Header("Cooldown")] 
    public float dashCd;

    // [Header("Input")] 
    // public KeyCode dashKey = KeyCode.Q;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovementTwo>();
        finalDuration = dashDuration;
    }

    public void Recalculate (int upgrades)
    {
        finalDuration = dashDuration + (0.25f * upgrades);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Dash"))
        {
            Dash();
        }

        if (pm.dashCdTimer > 0 && pm.grounded)
            pm.dashCdTimer -= Time.deltaTime;
        else if (pm.dashCdTimer > 0)
            pm.dashCdTimer -= Time.deltaTime / 4;
    }

    private void Dash()
    {
        if (pm.dashCdTimer > 0) return;
        else pm.dashCdTimer = dashCd;
        
        pm.dashing = true;
        cam.DoFov(dashFov);
        pm.maxYSpeed = maxDashYSpeed;
        
        Transform forwardT;

        if (useCameraForward)
            forwardT = playerCam;
        else 
            forwardT = orientation;

        Vector3 direction = GetDirection(forwardT);
        
        Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;

        if (disableGravity)
            rb.useGravity = false;
        
        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);
        
        Invoke(nameof(ResetDash), finalDuration);
    }

    private Vector3 delayedForceToApply;

    private void DelayedDashForce()
    {

        if (resetVel)
            rb.velocity = Vector3.zero;
        
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/dash");
    }
    void ResetDash()
    {
        pm.dashing = false;
        pm.maxYSpeed = 0;
        
        cam.DoFov(75f);

        if (disableGravity)
            rb.useGravity = true;
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirections)
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else 
            direction = forwardT.forward;

        if (verticalInput == 0 && horizontalInput == 0)
            direction = forwardT.forward;

        return direction.normalized;
    }
}