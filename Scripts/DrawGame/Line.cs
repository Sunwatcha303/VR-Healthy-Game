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
    private bool isOutSide = false;
    private bool isDrawing = false;
    private Vector3 prePos;
    private Vector3 prePosR;
    private Vector3 prePosL;
    private Vector3 startPos;
    public float minDistance = 0.01f;

    public bool isLeft;
    public bool isRight;

    public GameObject toggleLeft;
    public GameObject toggleRight;

    public GameObject leftHand;
    public GameObject rightHand;

    public DrawGameController gameController;
    public AlertMessage alertMessage;
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

        rightHand.SetActive(true); 
        leftHand.SetActive(false);

        SetHand();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(gameController.getStart() + " isdrawing " + isDrawing);
        
        if (gameController.getStart() && isDrawing && !currentHand.Candraw())
        {
            if (!isOutSide)
            {
                Debug.Log("Now out side the box");
                isOutSide = true;
                gameController.SetStartTimeOutTheBox(Time.time);
            }
        }
        if (!isDrawing && currentHand != null && currentHand.IsPointToStart())
        {
            isDrawing = true;
            gameController.setStart(true);
            gameController.setActiveCirclePointToStart(false);

            prePos = currentHand.linePointer.GetPosition(1);
            lineRenderer.positionCount = 0;
            startPos = prePos;
        }

        if (isDrawing)
        {
            if (currentHand.Candraw())
            {
                Vector3 curPos = currentHand.linePointer.GetPosition(1);
                curPos.z -= 0.01f;
                float dist = Vector3.Distance(curPos, prePos);
                if (isOutSide && dist < minDistance + 0.5f)
                {
                    Debug.Log("Now in side the box");
                    isOutSide = false;
                    gameController.SetEndTimeOutTheBox(Time.time);
                }
                if (!isOutSide && dist > minDistance)
                {
                    lineRenderer.positionCount++;
                    lineRenderer.SetPosition(lineRenderer.positionCount - 1, curPos);
                    prePos = curPos;
                }

                if (!isOutSide && lineRenderer.positionCount > 50 && Vector3.Distance(startPos, curPos) < 0.5f)
                {
 
                    lineRenderer.loop = true;
                    Debug.Log("EndGame: Finish");
                    alertMessage.ShowAlert("EndGame() It loop", 3);
                    EndGame();
                }
            }
            /*else if (!currentHand.Candraw())
            {
                Debug.Log("EndGame() Out of area");
                alertMessage.ShowAlert("EndGame() Can't draw", 3);

                EndGame();
            }*/
        }
        if (!gameController.getStart())
        {
            isDrawing = false;
        }
    }

    void EndGame()
    {
        isDrawing = false;
        //toggleRight.GetComponent<Toggle>().isOn = true;
        //toggleLeft.GetComponent<Toggle>().isOn = false;
        //isRight = true;
        //isLeft = false;
        //rightHand.SetActive(true);
        //leftHand.SetActive(false);
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
    public void ToggleChange2()
    {
        if (toggleRight.GetComponent<Toggle>().isOn)
        {
            toggleLeft.GetComponent<Toggle>().isOn = false; // Turn off left toggle
            isRight = true;
            rightHand.SetActive(true);
            isLeft = false; // Ensure left is off
            leftHand.SetActive(false); // Turn off left hand if it was on
        }
        Debug.Log("isLeft: " + isLeft + ", isRight: " + isRight);
        SetHand();
    }
    public void ToggleChange1()
    {
        if (toggleLeft.GetComponent<Toggle>().isOn)
        {
            toggleRight.GetComponent<Toggle>().isOn = false; // Turn off right toggle
            isLeft = true;
            leftHand.SetActive(true);
            isRight = false; // Ensure right is off
            rightHand.SetActive(false); // Turn off right hand if it was on
        }
        Debug.Log("isLeft: " + isLeft + ", isRight: " + isRight);
        SetHand();
    }

    public void SetWidthLine(float size)
    {
        lineRenderer.startWidth = size;
        lineRenderer.endWidth = size;
    }

    public void SetHand()
    {
        if (/*gameController.getStart() && */isRight /* && !isDrawing && pointerVisualizerR.Candraw()*/)
        {
            isDrawing = false;
            //prePos = pointerVisualizerR.linePointer.GetPosition(1);
            lineRenderer.positionCount = 0;
            currentHand = pointerVisualizerR;
            //startPos = prePos;
        }
        if (/*gameController.getStart() && */isLeft /*&& !isDrawing && pointerVisualizerL.Candraw()*/)
        {
            isDrawing = false;
            //prePos = pointerVisualizerL.linePointer.GetPosition(1);
            lineRenderer.positionCount = 0;
            currentHand = pointerVisualizerL;
            //startPos = prePos;
        }
    }

    public void SetIsOutSide(bool b)
    {
        isOutSide = b;
    }

}
