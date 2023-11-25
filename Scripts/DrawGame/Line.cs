using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Line : MonoBehaviour
{
    // Start is called before the first frame update
    public Pointer pointerVisualizerR;
    public Pointer pointerVisualizerL;
    Pointer currentHand;
    internal LineRenderer lineRenderer;
    private bool isDrawing = false;
    private Vector3 prePos;
    private Vector3 prePosR;
    private Vector3 prePosL;
    private Vector3 startPos;
    public float minDistance = 0.001f;

    public DrawGameController gameController;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        prePosR = pointerVisualizerR.linePointer.GetPosition(1);
        prePosL = pointerVisualizerL.linePointer.GetPosition(1);
        lineRenderer.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.getStart() && OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            isDrawing = true;
            prePos = pointerVisualizerR.linePointer.GetPosition(1);
            lineRenderer.positionCount = 0;
            currentHand = pointerVisualizerR;
            startPos = prePos;
        }
        if (gameController.getStart() && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            isDrawing = true;
            prePos = pointerVisualizerL.linePointer.GetPosition(1);
            lineRenderer.positionCount = 0;
            currentHand = pointerVisualizerL;
            startPos = prePos;
        }
        if (gameController.getStart() && OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            isDrawing = false;
        }
        if (gameController.getStart() && OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            isDrawing = false;
        }
        if (isDrawing)
        {
            if (currentHand.linePointer.positionCount > 1)
            {
                Vector3 curPos = currentHand.linePointer.GetPosition(1);

                if (Vector3.Distance(curPos, prePos) > minDistance)
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, curPos);
                    prePos = curPos;
                }

                if (lineRenderer.positionCount > 10 && Vector3.Distance(startPos, curPos) < 0.05f)
                {
                    lineRenderer.loop = true;
                    EndGame();
                }
            }
            else
            {
                isDrawing = false;
            }
        }
    }

    void EndGame()
    {
        isDrawing = false;
        gameController.SetFinishGame(lineRenderer);
    }

    public void SetLinePositionCount(int n)
    {
        lineRenderer.positionCount = n;
    }

    public void SetLineRendererLoop(bool b)
    {
        lineRenderer.loop = b;
    }
}
