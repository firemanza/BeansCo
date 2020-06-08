using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask grappleable;
    public Transform guntip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopGrapple();
        }
    }
    private void LateUpdate()
    {
        drawRope();
    }
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position , camera.forward, out hit, maxDistance, grappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float dist = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = dist * 0.8f;
            joint.minDistance = dist * 0.25f;

            joint.spring = 15f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
        }
    }
    void drawRope()
    {
        if (!joint) return;

        lr.SetPosition(0, guntip.position);
        lr.SetPosition(1, grapplePoint);
    }
    void StopGrapple()
    {
        lr.positionCount = 0;
        Destroy(joint);
    }
    public bool isGrappling()
    {
        return joint != null;
    }
    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }
}
