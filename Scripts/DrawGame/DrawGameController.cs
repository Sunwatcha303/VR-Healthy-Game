using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
public class DrawGameController : MonoBehaviour
{
    // Start is called before the first frame update
    // public float radiusIn = 3.25f, radiusOut = 5f, width = 0.1721306f;
    public GameObject AccurateText;
    public GameObject timeText;
    public GameObject timeOutSideText;
    public GameObject center;
    public GameObject finishScene;
    public GameObject playScene;

    public GameObject pointToStartObj;
    public GameObject UIFinished;

    public bool isStart = false;
    public int time = 0;
    public Line line;
    public LineLine lineline;
    public LoggingDrawGame logging;

    public GameObject selectMenu;
    public GameObject selectLevel;

    float timer;
    float timeOutTheBox = 0;
    private bool circleCheck = false, squareCheck = false, triangleCheck = false;

    public DrawObjOnBoard circle;
    public DrawObjOnBoard square;
    public DrawObjOnBoard tri;
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
            accurate = Accuracy.CalculateAccurateCircle(lineRenderer, circle);
            pic = 0;
        }
        else if (squareCheck)
        {
            accurate = Accuracy.CalculateAccurateSquare(lineRenderer, square);
            pic = 1;
        }
        else if (triangleCheck)
        {
            accurate = Accuracy.CalculateAccurateTriangle(lineRenderer);
            pic = 2;
        }
        //AccurateText.GetComponent<TextMeshProUGUI>().text = (accurate * 100).ToString("F2") + "%";
        timeText.GetComponent<TextMeshProUGUI>().text = "Total Time: " + String.Format("{0:0.00}", timer) + " sec";
        timeOutSideText.GetComponent<TextMeshProUGUI>().text = "Time out side: " + String.Format("{0:0.00}", timeOutTheBox) + " sec";
        selectMenu.SetActive(false);
        finishScene.SetActive(true);
        logging.SaveToLog(pic, accurate*100, timer, timeOutTheBox);

        UIFinished.SetActive(true);

        timeOutTheBox = 0;
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

    public void SetStartTimeOutTheBox(float time)
    {
        PlayerPrefs.SetFloat("timeOutTheBox", time);
    }

    public void SetEndTimeOutTheBox(float time)
    {
        timeOutTheBox += time - PlayerPrefs.GetFloat("timeOutTheBox");
        Debug.Log("time out the box: " + timeOutTheBox);
        PlayerPrefs.SetFloat("timeOutTheBox", time);
    }

    public void SelectLevel()
    {
        int level = selectLevel.GetComponent<TMP_Dropdown>().value;
        if(level == 0)
        {
            circle.SetSize(1.75f, 3f);
            square.SetSize(2f, 4f);
            tri.SetSize(2f, 4f);
            line.SetWidthLine(0.75f);
            lineline.SetWidthLine(0.75f);
        }
        else if(level == 1)
        {
            circle.SetSize(2f, 3f);
            square.SetSize(2.5f, 4f);
            tri.SetSize(2.5f, 4f);
            line.SetWidthLine(0.5f);
            lineline.SetWidthLine(0.5f);
        }
        else
        {
            circle.SetSize(2.25f, 3f);
            square.SetSize(2.75f, 4f);
            tri.SetSize(2.75f, 4f);
            line.SetWidthLine(0.25f);
            lineline.SetWidthLine(0.25f);
        }
    }

    internal void setActiveCirclePointToStart(bool v)
    {
        //pointToStartObj.transform.GetChild(0).gameObject.SetActive(false);
        //pointToStartObj.transform.GetChild(1).gameObject.SetActive(false);
        pointToStartObj.SetActive(v);
    }

    public void setUIFinished(bool v)
    {
        UIFinished.SetActive(v);
    }

    public void ResetTimeOutTheBox(float i)
    {
        timeOutTheBox = i;
        SetStartTimeOutTheBox(Time.time);
    }
}
