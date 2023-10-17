using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour
{
    public GrappleGun grappleGun;

    private Quaternion desiredRotation;
    private float rotationSpeed = 5f;
    void Update()
    {
        if (!grappleGun.IsGrappling())
        {
            desiredRotation = transform.parent.rotation;
        }

        else
        {
            desiredRotation = Quaternion.LookRotation(grappleGun.GetGrapplePoint() - transform.position);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }
}
