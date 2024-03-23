using System.Collections;
using System.Collections.Generic;
using OVRSimpleJSON;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

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

    public bool isLeft;
    public bool isRight;

    public GameObject toggleLeft;
    public GameObject toggleRight;
    public DrawGameController gameController;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        prePosR = pointerVisualizerR.linePointer.GetPosition(1);
        prePosL = pointerVisualizerL.linePointer.GetPosition(1);
        lineRenderer.positionCount = 0;

        toggleRight.GetComponent<Toggle>().isOn = true;
        toggleLeft.GetComponent<Toggle>().isOn = false;
        isRight = true;
        isLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.getStart() && isRight)
        {
            isDrawing = true;
            prePos = pointerVisualizerR.linePointer.GetPosition(1);
            lineRenderer.positionCount = 0;
            currentHand = pointerVisualizerR;
            startPos = prePos;
        }
        if (gameController.getStart() && isLeft)
        {
            isDrawing = true;
            prePos = pointerVisualizerL.linePointer.GetPosition(1);
            lineRenderer.positionCount = 0;
            currentHand = pointerVisualizerL;
            startPos = prePos;
        }
        if (isDrawing)
        {
            if (currentHand.CalculateEnd().HasValue && currentHand.linePointer.positionCount > 0)
            {
                Vector3 curPos = currentHand.linePointer.GetPosition(1);

                if (Vector3.Distance(curPos, prePos) > minDistance)
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, curPos);
                    prePos = curPos;
                }

                if (lineRenderer.positionCount > 100 && Vector3.Distance(startPos, curPos) < 0.1)
                {
                    lineRenderer.loop = true;
                    EndGame();
                }
            }
            if(!currentHand.CalculateEnd().HasValue)
            {
                isDrawing = false;
                EndGame();
            }
        }
    }

    void EndGame()
    {
        isDrawing = false;
        toggleRight.GetComponent<Toggle>().isOn = true;
        toggleLeft.GetComponent<Toggle>().isOn = false;
        isRight = true;
        isLeft = false;
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
    void Toggle1Changed()
    {
        toggleRight.GetComponent<Toggle>().isOn = false;
        Debug.Log("isLeft: " + isLeft + ", isRight: " + isRight);
    }

    void Toggle2Changed()
    {
        toggleLeft.GetComponent<Toggle>().isOn = false;
        Debug.Log("isLeft: " + isLeft + ", isRight: " + isRight);
    }


}
