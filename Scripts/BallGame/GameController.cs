using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour

{
    bool isStart = false;
    public GameObject player;
    public GameObject mainCamera;
    public GameObject mainMenu, endGameMenu;
    public GameObject playScene;
    public GameObject boardFreeMode;
    public GameObject setTimer;
    public GameObject timeText;
    public TextMesh scoreLH, scoreRH, timeLB;
    public TextMeshProUGUI totalScore, scoreLeftText, scoreRightText;
    float timer;
    float timeForRandMode;
    public SpawnBall spawnBall;
    public Logging loggin;
    
    private int scoreLeft = 0;
    private int scoreRight = 0;
    private int score = 0;

    public GameObject selectModeMenu;

    public GameObject nearDistObj;
    public GameObject normalDistObj;
    public GameObject farDistObj;

    public GameObject startButton;
    public GameObject backButton;
    public GameObject leftHandButton;
    public GameObject rightHandButton;

    public GameObject leftHandObj;
    public GameObject rightHandObj;

    public GameObject setModeButton;
    public GameObject randModeButton;
    public GameObject freeModeButton;

    public GameObject UISetMode;
    public GameObject UIRandMode;
    public GameObject UIFreeMode;
    public GameObject setAndRandMode;
    public GameObject UIBoardFreeMode;

    public GameObject finishButton;
    public GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        playScene.SetActive(false);
        PlayerPrefs.SetFloat("nextClick", Time.time);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
 
        nearDistObj.GetComponent<Button>().onClick.AddListener(onClickNearDistObj);
        normalDistObj.GetComponent<Button>().onClick.AddListener(onClickNormalDistObj);
        farDistObj.GetComponent<Button>().onClick.AddListener(onClickFarDistObj);

        startButton.GetComponent<Button>().onClick.AddListener(onClickStartButton);
        backButton.GetComponent<Button>().onClick.AddListener(onClickBackButton);
        leftHandButton.GetComponent<Button>().onClick.AddListener(onClickLeftHandButton);
        rightHandButton.GetComponent<Button>().onClick.AddListener(onClickRightHandButton);

        setModeButton.GetComponent<Button>().onClick.AddListener(onClickSetModeButton);
        randModeButton.GetComponent<Button>().onClick.AddListener(onClickRandModeButton);
        freeModeButton.GetComponent<Button>().onClick.AddListener(onClickFreeButton);

        finishButton.GetComponent<Button>().onClick.AddListener(onClickFinishButton);
        restartButton.GetComponent<Button>().onClick.AddListener(onClickRestartButton);

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

        player.transform.position = new Vector3(0.0f, 1.0f, 0.0f);

    }
    private void onClickRestartButton()
    {
        endGameMenu.SetActive(false);
        if (spawnBall.GetIsFreeMode())
        {
            setAndRandMode.SetActive(false);
        }
        else
        {
            setAndRandMode.SetActive(true);
        }
        selectModeMenu.SetActive(true);
    }

    private void onClickFinishButton()
    {
        setEndGame();
        SetActiveSetAndRandMode(false);
    }

    private void onClickSetModeButton()
    {
        setModeButton.GetComponent<Image>().color = new Color(1f, 178f / 255f, 0f, 1);
        setModeButton.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.white;
        UISetMode.SetActive(true);
        spawnBall.SetIsSetMode(true);

        randModeButton.GetComponent<Image>().color = Color.gray;
        randModeButton.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.black;
        UIRandMode.SetActive(false);
        spawnBall.SetRandMode(false);

        freeModeButton.GetComponent<Image>().color = Color.gray;
        freeModeButton.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.black;
        UIFreeMode.SetActive(false);
        spawnBall.SetFreeMode(false);

        setAndRandMode.SetActive(true);
    }

    private void onClickRandModeButton()
    {
        setModeButton.GetComponent<Image>().color = Color.gray;
        setModeButton.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.black;
        UISetMode.SetActive(false);
        spawnBall.SetIsSetMode(false);

        randModeButton.GetComponent<Image>().color = new Color(1f, 178f / 255f, 0f, 1);
        randModeButton.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.white;
        UIRandMode.SetActive(true);
        spawnBall.SetRandMode(true);

        freeModeButton.GetComponent<Image>().color = Color.gray;
        freeModeButton.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.black;
        UIFreeMode.SetActive(false);
        spawnBall.SetFreeMode(false);

        setAndRandMode.SetActive(true);
    }

    private void onClickFreeButton()
    {
        setModeButton.GetComponent<Image>().color = Color.gray;
        setModeButton.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.black;
        UISetMode.SetActive(false);
        spawnBall.SetIsSetMode(false);

        randModeButton.GetComponent<Image>().color = Color.gray;
        randModeButton.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.black;
        UIRandMode.SetActive(false);
        spawnBall.SetRandMode(false);

        freeModeButton.GetComponent<Image>().color = new Color(1f, 178f / 255f, 0f, 1);
        freeModeButton.transform.GetChild(0).GetComponent<TMP_Text>().color = Color.white;
        UIFreeMode.SetActive(true);
        spawnBall.SetFreeMode(true);

        setAndRandMode.SetActive(false);
    }

    private void onClickRightHandButton()
    {
        if (spawnBall.GetIsRightHand() && !spawnBall.GetIsLeftHand())
        {
            rightHandButton.GetComponent<Image>().color = Color.white;
            spawnBall.setIsRightHand(true);
            rightHandObj.SetActive(true);

            leftHandButton.GetComponent<Image>().color = Color.white;
            spawnBall.setIsLeftHand(true);
            leftHandObj.SetActive(true);
        }
        else if(spawnBall.GetIsRightHand() && spawnBall.GetIsLeftHand())
        {
            rightHandButton.GetComponent<Image>().color = Color.gray;
            spawnBall.setIsRightHand(false);
            rightHandObj.SetActive(false);
        }
        else
        {
            rightHandButton.GetComponent<Image>().color = Color.white;
            spawnBall.setIsRightHand(true);
            rightHandObj.SetActive(true);
        }
        
    }

    private void onClickLeftHandButton()
    {
        if (spawnBall.GetIsLeftHand() && !spawnBall.GetIsRightHand())
        {
            leftHandButton.GetComponent<Image>().color = Color.white;
            spawnBall.setIsLeftHand(true);
            leftHandObj.SetActive(true);

            rightHandButton.GetComponent<Image>().color = Color.white;
            spawnBall.setIsRightHand(true);
            rightHandObj.SetActive(true);
        }
        else if(spawnBall.GetIsLeftHand() && spawnBall.GetIsRightHand())
        {
            leftHandButton.GetComponent<Image>().color = Color.gray;
            spawnBall.setIsLeftHand(false);
            leftHandObj.SetActive(false);
        }
        else
        {
            leftHandButton.GetComponent<Image>().color = Color.white;
            spawnBall.setIsLeftHand(true);
            leftHandObj.SetActive(true);
        }
    }

    private void onClickBackButton()
    {
        selectModeMenu.SetActive(false);
        mainMenu.SetActive(true);
        setAndRandMode.SetActive(false);
    }

    private void onClickStartButton()
    {
        spawnBall.SetStartToSpawnBall();
        setStart();

        selectModeMenu.SetActive(false);

        if (spawnBall.GetIsFreeMode())
        {
            setAndRandMode.SetActive(true);
        }
        else
        {
            setAndRandMode.SetActive(false);
        }

        playScene.SetActive(true);
    }

    public void setStart()
    {
        isStart = true;

        Time.timeScale = 1;

        timer = (spawnBall.isQSystem) ? timeForRandMode + Time.time : Time.time;

        playScene.SetActive(true);

        SetCursorFreeMode();

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

        boardFreeMode.SetActive(false);
        playScene.SetActive(false);

        //mainCamera.SetActive(false);
        //mainMenu.SetActive(true);
        endGameMenu.SetActive(true);
        totalScore.text = "Total Score: " + (scoreLeft+scoreRight);
        scoreLeftText.text = "Score Left Hand: " + scoreLeft;
        scoreRightText.text = "Score Right Hand: " + scoreRight;
        timeText.GetComponent<TextMeshProUGUI>().text = "Time: " + String.Format("{0:0.00}", Mathf.Abs(timer - Time.time));
        if (spawnBall.isQSystem)
        {
            timeText.SetActive(false);
        }
        else
        {
            timeText.SetActive(true);
        }
        spawnBall.DestroyBall();
        loggin.SaveToLog(
            (spawnBall.isQSystem)? "random": (spawnBall.freeMode ? "free": "set"),
            scoreLeft,
            scoreRight,
            (spawnBall.isQSystem)? timeForRandMode : Mathf.Abs(timer - Time.time),
            GetComponent<ModeToPlay>().leftFlag,
            GetComponent<ModeToPlay>().rightFlag,
            spawnBall.getDistance()
        );

        score = 0;
        scoreLeft = 0;
        scoreRight = 0;

        //spawnBall.setQSystem(false);
        //spawnBall.SetFreeMode(false);
        //spawnBall.setPosZ("near");
    }

    public void setFinish()
    {
        isStart = false;

        playScene.SetActive(false);
        boardFreeMode.SetActive(false);

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
        return timeForRandMode;
    }

    public int getScore()
    {
        return score;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetCursorFreeMode()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void onClickNearDistObj()
    {
        nearDistObj.GetComponent<Image>().color = Color.white;
        normalDistObj.GetComponent<Image>().color = Color.gray;
        farDistObj.GetComponent<Image>().color = Color.gray;

        spawnBall.SetIsNear();
    }
    public void onClickNormalDistObj()
    {
        nearDistObj.GetComponent<Image>().color = Color.gray;
        normalDistObj.GetComponent<Image>().color = Color.white;
        farDistObj.GetComponent<Image>().color = Color.gray;

        spawnBall.SetIsNormal();
    }
    public void onClickFarDistObj()
    {
        nearDistObj.GetComponent<Image>().color = Color.gray;
        normalDistObj.GetComponent<Image>().color = Color.gray;
        farDistObj.GetComponent<Image>().color = Color.white;

        spawnBall.SetIsFar();
    }

    public void SetActiveBoardFreeMode(bool b)
    {
        boardFreeMode.SetActive(b);
    }

    internal void setTimerForRand(int t)
    {
        timeForRandMode = t;
    }

    internal void SetActiveSetAndRandMode(bool v)
    {
        setAndRandMode.SetActive(v);
    }
}
