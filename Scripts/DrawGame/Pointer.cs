using System.Collections;
using System.Collections.Generic;

using System;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [Tooltip("Object which points with Z axis. E.g. CentreEyeAnchor from OVRCameraRig")]
    public Transform rayTransform;

    [Header("Visual Elements")]
    [Tooltip("Line Renderer used to draw selection ray.")]
    public LineRenderer linePointer = null;

    [Tooltip("Visually, how far out should the ray be drawn.")]
    public float rayDrawDistance = 2.5f;

    void Update()
    {
        linePointer.enabled = (OVRInput.GetActiveController() == OVRInput.Controller.Touch);
        Ray ray = new Ray(rayTransform.position, rayTransform.forward);
        linePointer.SetPosition(0, ray.origin);
        if (CalculateEnd() != null)
        {
            linePointer.SetPosition(1, CalculateEnd().Value);
        }
    }

    private Vector3 DefaultDistance()
    {
        return rayTransform.position + (rayTransform.forward * rayDrawDistance);
    }

    public Vector3? CalculateEnd()
    {
        RaycastHit hit = CreateForwardRayCast();

        if (hit.collider.CompareTag("ToDraw"))
        {
            Vector3 endPos = hit.point;
            endPos.z -= 0.1f;
            return endPos;
        }

        return null;
    }

    private RaycastHit CreateForwardRayCast()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(rayTransform.position, rayTransform.forward);

        Physics.Raycast(ray, out hit, rayDrawDistance);
        return hit;
    }
}
