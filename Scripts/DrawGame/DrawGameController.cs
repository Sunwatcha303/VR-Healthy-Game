using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;
public class DrawGameController : MonoBehaviour
{
    // Start is called before the first frame update
    // public float radiusIn = 3.25f, radiusOut = 5f, width = 0.1721306f;
    public GameObject AccurateText;
    public GameObject timeText;
    public GameObject center;
    public bool isStart = false;
    public int time = 0;
    public GameObject finishScene;
    public GameObject playScene;
    public GameObject Camera;
    public Line line;
    public LoggingDrawGame logging;
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
        PlayerPrefs.SetFloat("time", Time.time);
        isStart = b;
    }

    public bool getStart()
    {
        return isStart;
    }

    public void SetFinishGame(LineRenderer lineRenderer)
    {
        int pic = 0;
        timer = Time.time - PlayerPrefs.GetFloat("time");
        isStart = false;
        float accurate = 0;
        if (circleCheck)
        {
            accurate = calculateAccurateCircle(lineRenderer);
            pic = 0;
        }
        else if (squareCheck)
        {
            accurate = calculateAccurateSquare(lineRenderer);
            pic = 1;
        }
        else
        {
            accurate = calculateAccurateTriangle(lineRenderer);
            pic = 2;
        }
        AccurateText.GetComponent<TextMeshProUGUI>().text = (accurate * 100).ToString("F2") + "%";
        timeText.GetComponent<TextMeshProUGUI>().text = "Time: " + String.Format("{0:0.00}", timer);
        finishScene.SetActive(true);
        Camera.SetActive(false);
        logging.SaveToLog(pic, accurate*100, timer);
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
    bool IsPointInTriangle(Vector3 p, Vector3 p0, Vector3 p1, Vector3 p2)
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

    public float calculateAccurateCircle(LineRenderer lineRenderer)
    {
        float radiusIn = 1.225f/2, radiusOut = 1.885f/2, width = 0.04303265f;
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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
