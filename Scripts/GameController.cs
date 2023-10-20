using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour

{
    bool isStart = false;
    public int score = 0;
    public GameObject mainCamera;
    public GameObject mainMenu, endGameMenu;
    public GameObject playScene;
    public GameObject setTimer;
    public TextMesh scoreLB, timeLB;
    public TextMeshProUGUI totalScore;
    float timer;
    float time;
    public SpawnBall spawnBall;
    // Start is called before the first frame update
    void Start()
    {
        playScene.SetActive(false);
        PlayerPrefs.SetFloat("nextClick", Time.time);
        Time.timeScale = 0;

        string input = setTimer.GetComponent<TMP_InputField>().text;
        int t = (input == "") ? 0 : int.Parse(input);
        time = t;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        scoreLB.text = "Score : " + score;
        timeLB.text = "Time : " + String.Format("{0:0.00}", timer - Time.time);

        if (Time.time > timer && isStart)
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
        timer = time + Time.time;

        playScene.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void setScore()
    {
        score += 1;
    }
    public void setEndGame()
    {
        isStart = false;

        playScene.SetActive(false);

        mainCamera.SetActive(false);
        mainMenu.SetActive(true);
        endGameMenu.SetActive(true);
        totalScore.text = "Total Score: " + score;
        spawnBall.DestroyBall();

        GetComponent<Logging>().SaveToLog(score, time, spawnBall.getSizeListBall(), GetComponent<ModeToPlay>().leftFlag, GetComponent<ModeToPlay>().rightFlag, spawnBall.getDistance());

        score = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void setFinish()
    {
        isStart = false;

        playScene.SetActive(false);

        score = 0;

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
}
