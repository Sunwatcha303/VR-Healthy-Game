using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour

{
    bool isStart = false;
    bool isFinish = false;
    public int score = 0;
    public int time = 60;
    public GameObject playScene;
    public TextMesh scoreLB, timeLB;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        // startScene.SetActive(true);

        playScene.SetActive(false);
        PlayerPrefs.SetFloat("nextClick", Time.time);
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStart)
        {
            // startScene.SetActive(false);
            playScene.SetActive(true);
        }
        else
        {
            // startScene.SetActive(true);
            playScene.SetActive(false);
        }
        scoreLB.text = "Score : " + score;
        timeLB.text = "Time : " + String.Format("{0:0.00}", timer - Time.time);

        if (Time.time > timer && isStart)
        {
            isFinish = true;
        }

        if (isFinish)
        {
            Time.timeScale = 0;
            // StartCoroutine(ReSetLevel());
        }

    }
    IEnumerator ReSetLevel()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerPrefs.SetInt("score", score);
        if (PlayerPrefs.HasKey("bestScore"))
        {
            if (PlayerPrefs.GetInt("bestScore") < score)
            {
                PlayerPrefs.SetInt("bestScore", score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("bestScore", score);
        }
        isStart = false;
        isFinish = false;

    }
    public void getTriggerStart()
    {
        Time.timeScale = 1;
        if (!isStart)
        {
            timer = time + Time.time;
        }
        isStart = true;
        isFinish = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void getScore()
    {
        score += 1;
    }

    public void setFinish()
    {
        isFinish = true;
        isStart = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public bool getStart()
    {
        return isStart;
    }
}
