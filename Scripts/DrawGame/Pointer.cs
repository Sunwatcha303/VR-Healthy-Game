using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Meta.WitAi;

public class Pointer : MonoBehaviour
{
    [Tooltip("Object which points with Z axis. E.g. CentreEyeAnchor from OVRCameraRig")]
    public Transform rayTransform;

    [Header("Visual Elements")]
    [Tooltip("Line Renderer used to draw selection ray.")]
    public LineRenderer linePointer = null;

    [Tooltip("Visually, how far out should the ray be drawn.")]
    public float rayDrawDistance = 5f;

    void Update()
    {
        linePointer.enabled = (OVRInput.GetActiveController() == OVRInput.Controller.Touch);
        Ray ray = new Ray(rayTransform.position, rayTransform.forward);
        linePointer.SetPosition(0, ray.origin);
        linePointer.SetPosition(1, CalculateEnd());
    }
    private Vector3 DefaultDistance()
    {
        return rayTransform.position + (rayTransform.forward * rayDrawDistance);
    }

    public Vector3 CalculateEnd()
    {
        RaycastHit hit = CreateForwardRayCast();

        if (hit.collider != null)
        {
            return hit.point;
        }

        // Return a default position if no collider is hit
        return DefaultDistance();
    }


    private RaycastHit CreateForwardRayCast()
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(rayTransform.position, rayTransform.forward);

        Physics.Raycast(ray, out hit, rayDrawDistance);
        return hit;
    }

    public bool Candraw()
    {
        RaycastHit hit = CreateForwardRayCast();
        if (hit.collider != null && hit.collider.CompareTag("ToDraw"))
        {
            return true;
        }
        //Debug.Log(hit.point);
        return false;
    }

    public bool IsPointToStart()
    {
        RaycastHit hit = CreateForwardRayCast();

        if (hit.collider != null && hit.collider.CompareTag("PointToStart"))
        {
            return true;
        }
        return false;
    }
}
