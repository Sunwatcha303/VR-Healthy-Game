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

    public GameObject circleGameObject;
    public GameObject squareGameObject;
    public GameObject triGameObject;

    public Pointer pointerR;
    public Pointer pointerL;

    public GameObject easyLevelButton;
    public GameObject normalLevelButton;
    public GameObject hardLevelButton;

    public GameObject leftHandButton;
    public GameObject rightHandButton;

    int level;
    void Start()
    {
        PlayerPrefs.SetFloat("nextClick", Time.time);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        easyLevelButton.GetComponent<Button>().onClick.AddListener(onClickEasyLevelButton);
        normalLevelButton.GetComponent<Button>().onClick.AddListener(onClickNormalLevelButton);
        hardLevelButton.GetComponent<Button>().onClick.AddListener(onClickHardLevelButton);

        leftHandButton.GetComponent<Button>().onClick.AddListener(onClickLeftHandButton);
        rightHandButton.GetComponent<Button>().onClick.AddListener(onClickRightHandButton);
    }

    private void onClickRightHandButton()
    {
        if (line.GetIsRight() && !line.GetIsLeft())
        {
            line.SetIsRight(false);
            line.SetIsLeft(true);
            rightHandButton.GetComponent<Image>().color = Color.gray;
            leftHandButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            line.SetIsRight(true);
            line.SetIsLeft(false);
            rightHandButton.GetComponent<Image>().color = Color.white;
            leftHandButton.GetComponent<Image>().color = Color.gray;
        }
        line.SetHand();
    }

    private void onClickLeftHandButton()
    {
        if (line.GetIsLeft() && !line.GetIsRight())
        {
            line.SetIsLeft(false);
            line.SetIsRight(true);

            leftHandButton.GetComponent<Image>().color = Color.gray;
            rightHandButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            line.SetIsLeft(true);
            line.SetIsRight(false);
            leftHandButton.GetComponent<Image>().color = Color.white;
            rightHandButton.GetComponent<Image>().color = Color.gray;
        }
        line.SetHand();
    }

    private void onClickHardLevelButton()
    {
        level = 2;
        circle.SetSize(2.25f, 3f);
        square.SetSize(2.75f, 4f);
        tri.SetSize(2.75f, 4f);
        line.SetWidthLine(0.25f);
        lineline.SetWidthLine(0.25f);

        easyLevelButton.GetComponent<Image>().color = Color.gray;
        normalLevelButton.GetComponent<Image>().color = Color.gray;
        hardLevelButton.GetComponent<Image>().color = Color.white;

        resetLevel();

    }

    private void onClickNormalLevelButton()
    {
        level = 1;
        circle.SetSize(2f, 3f);
        square.SetSize(2.5f, 4f);
        tri.SetSize(2.5f, 4f);
        line.SetWidthLine(0.5f);
        lineline.SetWidthLine(0.5f);

        easyLevelButton.GetComponent<Image>().color = Color.gray;
        normalLevelButton.GetComponent<Image>().color = Color.white;
        hardLevelButton.GetComponent<Image>().color = Color.gray;

        resetLevel();

    }

    private void onClickEasyLevelButton()
    {
        level = 0;
        circle.SetSize(1.75f, 3f);
        square.SetSize(2f, 4f);
        tri.SetSize(2f, 4f);
        line.SetWidthLine(0.75f);
        lineline.SetWidthLine(0.75f);

        easyLevelButton.GetComponent<Image>().color = Color.white;
        normalLevelButton.GetComponent<Image>().color = Color.gray;
        hardLevelButton.GetComponent<Image>().color = Color.gray;

        resetLevel();
    }

    private void resetLevel()
    {
        line.lineRenderer.positionCount = 0;
        lineline.lineRenderer.positionCount = 0;

        isStart = false;

        pointToStartObj.SetActive(true);
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
        //logging.SaveToLog(pic, accurate*100, timer, timeOutTheBox);
        char hand = (line.isLeft ? 'L' : 'R');

        logging.SaveToLog(pic, level, timer, timeOutTheBox, hand);

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
        level = selectLevel.GetComponent<TMP_Dropdown>().value;
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

    internal void setPosBoard(float x, float y, float z)
    {
        circleGameObject.transform.position = new Vector3(x, y, z);
        squareGameObject.transform.position = new Vector3(x, y - 0.4f, z);
        triGameObject.transform.position = new Vector3(x, y, z);
        pointToStartObj.transform.position = new Vector3(x - 2.45f, (y - 0.85f), z - 0.01f);
    }

    internal void setRayDistance(float rayDist)
    {
        pointerL.SetRayDistance(rayDist);
        pointerR.SetRayDistance(rayDist);
    }
}
