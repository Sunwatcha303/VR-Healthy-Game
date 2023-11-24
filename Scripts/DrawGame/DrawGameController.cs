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
    public float radiusIn = 3.25f, radiusOut = 5f, width = 0.1721306f;
    public GameObject AccurateText;
    public GameObject center;
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
        float accurate = calculateAccurate(lineRenderer);
        AccurateText.GetComponent<TextMeshProUGUI>().text = (accurate * 100).ToString("F2") + "%";
        finishScene.SetActive(true);
        Camera.SetActive(false);
    }

    public float calculateAccurate(LineRenderer lineRenderer)
    {
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
}
