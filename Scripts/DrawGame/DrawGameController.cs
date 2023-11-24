using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using Random = UnityEngine.Random;

public class DrawGameController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isStart = false;
    public int time = 0;
    public GameObject[] boards;
    public GameObject finishScene;
    public GameObject playScene;
    public GameObject Camera;
    public Line line;
    float timer;
    void Start()
    {
        PlayerPrefs.SetFloat("nextClick", Time.time);
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

    public void SetFinishGame()
    {
        isStart = false;
        finishScene.SetActive(true);
        playScene.SetActive(false);
        Camera.SetActive(false);
    }
}
