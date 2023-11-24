using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;
using TMPro;
public class DrawGameController : MonoBehaviour
{
    // Start is called before the first frame update
    // public float radiusIn = 3.25f, radiusOut = 5f, width = 0.1721306f;
    public GameObject AccurateText;
    public GameObject center;
    public bool isStart = false;
    public int time = 0;
    public GameObject finishScene;
    public GameObject playScene;
    public GameObject Camera;
    public Line line;
    float timer;
    private bool circleCheck = false, squareCheck = false, triangleCheck = false;

    void Start()
    {
        PlayerPrefs.SetFloat("nextClick", Time.time);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setStart(bool b)
    {
        isStart = b;
    }

    public bool getStart()
    {
        return isStart;
    }

    public void SetFinishGame(LineRenderer lineRenderer)
    {
        isStart = false;
        float accurate = 0;
        if (circleCheck)
        {
            accurate = calculateAccurateCircle(lineRenderer);
        }
        else if (squareCheck)
        {
            accurate = calculateAccurateSquare(lineRenderer);
        }
        else
        {
            accurate = calculateAccurateTriangle(lineRenderer);
        }
        AccurateText.GetComponent<TextMeshProUGUI>().text = (accurate * 100).ToString("F2") + "%";
        finishScene.SetActive(true);
        Camera.SetActive(false);
    }

    private float calculateAccurateSquare(LineRenderer lineRenderer)
    {
        float leftIn = -0.945f, leftOut = -1.25f, rightIn = 0.945f, rightOut = 1.25f, topIn = 2.45f, topOut = 2.75f, bottomIn = 0.55f, bottomOut = 0.25f;
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

    private float calculateAccurateTriangle(LineRenderer lineRenderer)
    {
        return 0;
    }

    public float calculateAccurateCircle(LineRenderer lineRenderer)
    {
        float radiusIn = 0.8125f, radiusOut = 1.25f, width = 0.04303265f;
        int count = 0;
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        foreach (Vector3 position in positions)
        {
            float dist = Vector3.Distance(position, center.transform.position);
            if (dist >= (radiusIn + width) && dist <= (radiusOut - width))
            {
                count++;
            }
        }
        float rate = (float)count / lineRenderer.positionCount;
        return rate;
    }


    public void SelectCheck(int n)
    {
        if (n == 0)
        {
            circleCheck = true;
            squareCheck = false;
            triangleCheck = false;
        }
        else if (n == 1)
        {
            circleCheck = false;
            squareCheck = true;
            triangleCheck = false;
        }
        else
        {
            circleCheck = false;
            squareCheck = false;
            triangleCheck = true;
        }
    }
}
