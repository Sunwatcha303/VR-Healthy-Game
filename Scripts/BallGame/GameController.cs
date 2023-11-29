using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour

{
    bool isStart = false;
    public GameObject mainCamera;
    public GameObject mainMenu, endGameMenu;
    public GameObject playScene;
    public GameObject setTimer;
    public TextMesh scoreLH, scoreRH, timeLB;
    public TextMeshProUGUI totalScore, scoreLeftText, scoreRightText;
    float timer;
    float time;
    public SpawnBall spawnBall;
    
    private int scoreLeft = 0;
    private int scoreRight = 0;
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        playScene.SetActive(false);
        PlayerPrefs.SetFloat("nextClick", Time.time);
        Time.timeScale = 0;
        if (spawnBall.isQSystem)
        {
            string input = setTimer.GetComponent<TMP_InputField>().text;
            int t = (input == "") ? 0 : int.Parse(input);
            time = t;
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        scoreLH.text = "Score Left Hand: " + scoreLeft;
        scoreRH.text = "Score Right Hand: " + scoreRight;
        timeLB.text = "Time : " + String.Format("{0:0.00}", Mathf.Abs(timer - Time.time));

        if (spawnBall.isQSystem && Time.time > timer && isStart)
        {
            setEndGame();
        }

    }
    public void setStart()
    {
        isStart = true;

        Time.timeScale = 1;

        string input = setTimer.GetComponent<TMP_InputField>().text;
        int t = (input == "") ? 0 : int.Parse(input);
        time = t;
        timer = (spawnBall.isQSystem) ? time + Time.time : Time.time;

        playScene.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void setScore()
    {
        score += 1;

    }
    public void setScoreLeft()
    {
        scoreLeft += 1;
    }
    public void setScoreRight()
    {
        scoreRight += 1;
    }
    public void setEndGame()
    {
        isStart = false;

        playScene.SetActive(false);

        mainCamera.SetActive(false);
        mainMenu.SetActive(true);
        endGameMenu.SetActive(true);
        totalScore.text = "Total Score: " + (scoreLeft+scoreRight);
        scoreLeftText.text = "Score Left Hand: " + scoreLeft;
        scoreRightText.text = "Score Right Hand: " + scoreRight;
        spawnBall.DestroyBall();

        GetComponent<Logging>().SaveToLog((spawnBall.isQSystem)? "random":"set",
            scoreLeft,
            scoreRight,
            (spawnBall.isQSystem)? time : Mathf.Abs(timer - Time.time),
            (spawnBall.isQSystem)? spawnBall.q.Count + scoreLeft + scoreRight : scoreLeft + scoreRight,
            GetComponent<ModeToPlay>().leftFlag,
            GetComponent<ModeToPlay>().rightFlag,
            spawnBall.getDistance()
        );

        score = 0;
        scoreLeft = 0;
        scoreRight = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void setFinish()
    {
        isStart = false;

        playScene.SetActive(false);

        score = 0;
        scoreLeft = 0;
        scoreRight = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public bool getStart()
    {
        return isStart;
    }

    public float getTimer()
    {
        return time;
    }

    public int getScore()
    {
        return score;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
