using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accuracy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float CalculateAccurateSquare(LineRenderer lineRenderer, DrawObjOnBoard square)
    {
        //float leftIn = -0.945f, leftOut = -1.25f, rightIn = 0.945f, rightOut = 1.25f, topIn = 2.45f, topOut = 2.75f, bottomIn = 0.55f, bottomOut = 0.25f;
        float leftIn = -square.polygonRadius, leftOut = -square.centerRadius, rightIn = 0.945f, rightOut = 1.25f, topIn = 2.45f, topOut = 2.75f, bottomIn = 0.55f, bottomOut = 0.25f;
        int count = 0;
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        foreach (Vector3 position in positions)
        {
            if (position.x > leftOut && position.x < leftIn)
            {
                if (position.y < topOut)
                {
                    count++;
                }
                else if (position.y > bottomOut)
                {
                    count++;
                }
            }
            else if (position.x > rightIn && position.x < rightOut)
            {
                if (position.y < topOut)
                {
                    count++;
                }
                else if (position.y > bottomOut)
                {
                    count++;
                }
            }
            else if (position.y > topIn && position.y < topOut)
            {
                if (position.x < rightOut)
                {
                    count++;
                }
                else if (position.x > leftOut)
                {
                    count++;
                }
            }
            else if (position.y > bottomOut && position.y < bottomIn)
            {
                if (position.x < rightOut)
                {
                    count++;
                }
                else if (position.x > leftOut)
                {
                    count++;
                }
            }
            else
            {
                Debug.Log(position);
            }
        }
        float rate = (float)count / lineRenderer.positionCount;
        return rate;
    }

    public static float CalculateAccurateTriangle(LineRenderer lineRenderer)
    {
        Vector3 leftOut = new Vector3(-1.25f, 0.425f, 0f);
        Vector3 leftIn = new Vector3(-0.7f, 0.7375f, 0f);
        Vector3 topOut = new Vector3(0f, 2.59f, 0f);
        Vector3 topIn = new Vector3(0f, 1.95f, 0f);
        Vector3 rightOut = new Vector3(1.25f, 0.425f, 0f);
        Vector3 rightIn = new Vector3(0.7f, 0.7375f, 0f);
        int count = 0;
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        foreach (Vector3 position in positions)
        {
            bool isInsideBig = IsPointInTriangle(position, leftOut, topOut, rightOut);
            bool isInsideSmall = IsPointInTriangle(position, leftIn, topIn, rightIn);
            if (isInsideBig && !isInsideSmall)
            {
                count++;
            }
        }
        float rate = (float)count / lineRenderer.positionCount;
        return rate;
    }
    private static bool IsPointInTriangle(Vector3 p, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float s = p0.y * p2.x - p0.x * p2.y + (p2.y - p0.y) * p.x + (p0.x - p2.x) * p.y;
        float t = p0.x * p1.y - p0.y * p1.x + (p0.y - p1.y) * p.x + (p1.x - p0.x) * p.y;

        if ((s < 0) != (t < 0))
            return false;

        float A = -p1.y * p2.x + p0.y * (p2.x - p1.x) + p0.x * (p1.y - p2.y) + p1.x * p2.y;
        if (A < 0.0)
        {
            s = -s;
            t = -t;
            A = -A;
        }

        return s > 0 && t > 0 && (s + t) <= A;
    }

    public static float CalculateAccurateCircle(LineRenderer lineRenderer, DrawObjOnBoard circle)
    {
        //float radiusIn = 1.225f/2, radiusOut = 1.885f/2, width = 0.04303265f;
        float radiusIn = circle.polygonRadius, radiusOut = circle.centerRadius;
        int count = 0;
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        Debug.Log(radiusIn + " " + radiusOut);
        foreach (Vector3 position in positions)
        {
            float dist = Vector3.Distance(position, new Vector3(0, 2, 2));
            if (dist >= (radiusIn) && dist <= (radiusOut))
            {
                count++;
            }
        }
        float rate = (float)count / lineRenderer.positionCount;
        return rate;
    }
}
