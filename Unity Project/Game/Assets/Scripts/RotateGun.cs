﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class RotateGun : MonoBehaviour
{
    public GrapplingGun grappling;

    private Quaternion desiredRotation;
    private float rotationSpeed = 5f;

    private void Update()
    {
        if (!grappling.isGrappling())
        {
            desiredRotation = transform.parent.rotation;
        }
        else
        {
            desiredRotation = Quaternion.LookRotation(grappling.GetGrapplePoint() - transform.position);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }
}
