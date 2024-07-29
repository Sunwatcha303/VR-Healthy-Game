using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class LineLine : MonoBehaviour
{
    // Start is called before the first frame update
    public bool active = false;

    internal LineRenderer lineRenderer;
    private bool isDrawing = false;
    private bool isOutSide = false;
    private Vector3 prePos;
    private Vector3 startPos;
    public float minDistance = 0.001f;
    public Camera c;

    public DrawGameController gameController;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (isPointToStart())
            {
                gameController.setStart(true);
                gameController.setActiveCirclePointToStart(false);
            }
            if (gameController.getStart() && !isDrawing)
            {
                if (getMousePosition().HasValue)
                {
                    isDrawing = true;
                    lineRenderer.positionCount = 0;
                    startPos = getMousePosition().Value;
                }
            }
            if (gameController.getStart() && isDrawing && !getMousePosition().HasValue)
            {
                //isDrawing = false;
                //EndGame();
                if (!isOutSide)
                {
                    Debug.Log("Now out side the box");
                    isOutSide = true;
                    gameController.SetStartTimeOutTheBox(Time.time);
                }
            }
            if (isDrawing && gameController.getStart())
            {
                if (getMousePosition().HasValue)
                {
                    
                    Vector3 curPos = getMousePosition().Value;
                    float dist = Vector3.Distance(curPos, prePos);
                    Debug.Log("dist: "+dist);
                    Debug.Log("prePos: "+prePos);
                    Debug.Log("curPos: "+curPos);
                    if (isOutSide && dist < (minDistance + 0.5f))
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
                    if (!isOutSide && lineRenderer.positionCount > 30 && Vector3.Distance(startPos, curPos) < 0.5f)
                    {
                        lineRenderer.loop = true;
                        EndGame();
                    }
                }
            }
        }

    }

    void EndGame()
    {
        isDrawing = false;
        gameController.SetFinishGame(lineRenderer);
    }

    private Vector3? getMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;

        Ray ray = c.ScreenPointToRay(mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log(hit.point);
            if (hit.collider.CompareTag("ToDraw"))
            {
                Vector3 hitPosition = hit.point;
                hitPosition.z -= 0.1f;
                return hitPosition;
            }
        }

        //Debug.Log(hit.point);
        return null;
    }

    private bool isPointToStart()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = c.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("PointToStart"))
            {
                return true;
            }
        }
        return false;
    }

    public void SetLinePositionCount(int n)
    {
        lineRenderer.positionCount = n;
    }

    public void SetLineRendererLoop(bool b)
    {
        lineRenderer.loop = b;
    }

    public bool GetOutSideTheBox()
    {
        return isOutSide;
    }
    public void SetWidthLine(float size)
    {
        lineRenderer.startWidth = size;
        lineRenderer.endWidth = size;
    }

    public void SetIsOutSide(bool b)
    {
        isOutSide = b;
    }
}
