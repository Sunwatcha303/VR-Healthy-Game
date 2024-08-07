using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static PatientData;

public class ScoreBoard : MonoBehaviour
{
    public Database database;
    public Transform containerBallGame;
    public Transform rowScoreBoardBallGame;

    public Transform containerBallGameModeSet;
    public Transform rowScoreBoardBallGameModeSet;
    public Transform containerBallGameModeRandom;
    public Transform rowScoreBoardBallGameModeRandom;
    public Transform containerBallGameModeFree;
    public Transform rowScoreBoardBallGameModeFree;

    public Transform containerDrawGame;
    public Transform rowScoreBoardDrawGame;
    public int rowHeight = 80;

    public Transform noContentText;

    public TMP_Text patientLabel;
    public TMP_Text doctorLabel;

    private List<Transform> instantiateList = new List<Transform>();
    
    public void ShowTable()
    {
        string id = PlayerPrefs.GetString("currentId");
        ShowTableScoreBoardDrawGame(id);
        ShowTableScoreBoardBallGame(id);
        
        patientLabel.text = "Patient: " + database.GetNameById(id);
        doctorLabel.text = "Doctor: " + PlayerPrefs.GetString("userName");
    }
    
    public void ShowTableScoreBoardBallGame(string id)
    {
        PatientData patientData = database.GetPatientDataById(id);
        if(patientData != null )
        {
            List<PatientData.BallGameData> ballGameDatas = patientData.GetBallGameDataList();
            if(ballGameDatas.Count == 0 || patientData.getName().Equals("Guest"))
            {
                Transform noContent = Instantiate(noContentText, containerBallGameModeSet);
                instantiateList.Add(noContent);
                noContent = Instantiate(noContentText, containerBallGameModeFree);
                instantiateList.Add(noContent);
                noContent = Instantiate(noContentText, containerBallGameModeRandom);
                instantiateList.Add(noContent);
            }
            else
            {
                for (int i=0;i< ballGameDatas.Count; i++)
                {
                    PatientData.BallGameData ballGameData = ballGameDatas[i];
                    if(ballGameData.getMode().Equals("set")) 
                    { 
                        Transform rowGameObject = Instantiate(rowScoreBoardBallGameModeSet, containerBallGameModeSet);
                        instantiateList.Add(rowGameObject);
                        RectTransform rowRectTranform = rowGameObject.GetComponent<RectTransform>();
                        //rowRectTranform.anchoredPosition = new Vector3(0, -rowHeight * i, 0);
                        rowGameObject.gameObject.SetActive(true);

                        Debug.Log(rowGameObject.Find("Date"));
                        rowGameObject.Find("Date").GetComponent<TMP_Text>().text = ballGameData.getDate();
                        rowGameObject.Find("Range").GetComponent<TMP_Text>().text = ballGameData.getDist();
                        rowGameObject.Find("LeftHand").GetComponent<TMP_Text>().text = ballGameData.getLeftHand();
                        rowGameObject.Find("RightHand").GetComponent<TMP_Text>().text = ballGameData.getRightHand();
                        rowGameObject.Find("LeftScore").GetComponent<TMP_Text>().text = ballGameData.getLeftScore();
                        rowGameObject.Find("RightScore").GetComponent<TMP_Text>().text = ballGameData.getRightScore();
                        rowGameObject.Find("Time").GetComponent<TMP_Text>().text = ballGameData.getTime();
                    }
                    else if (ballGameData.getMode().Equals("random"))
                    {
                        Transform rowGameObject = Instantiate(rowScoreBoardBallGameModeRandom, containerBallGameModeRandom);
                        instantiateList.Add(rowGameObject);
                        RectTransform rowRectTranform = rowGameObject.GetComponent<RectTransform>();
                        //rowRectTranform.anchoredPosition = new Vector3(0, -rowHeight * i, 0);
                        rowGameObject.gameObject.SetActive(true);

                        Debug.Log(rowGameObject.Find("Date"));
                        rowGameObject.Find("Date").GetComponent<TMP_Text>().text = ballGameData.getDate();
                        rowGameObject.Find("Range").GetComponent<TMP_Text>().text = ballGameData.getDist();
                        rowGameObject.Find("LeftHand").GetComponent<TMP_Text>().text = ballGameData.getLeftHand();
                        rowGameObject.Find("RightHand").GetComponent<TMP_Text>().text = ballGameData.getRightHand();
                        rowGameObject.Find("LeftScore").GetComponent<TMP_Text>().text = ballGameData.getLeftScore();
                        rowGameObject.Find("RightScore").GetComponent<TMP_Text>().text = ballGameData.getRightScore();
                        rowGameObject.Find("Time").GetComponent<TMP_Text>().text = ballGameData.getTime();
                    }
                    else
                    {
                        Transform rowGameObject = Instantiate(rowScoreBoardBallGameModeFree, containerBallGameModeFree);
                        instantiateList.Add(rowGameObject);
                        RectTransform rowRectTranform = rowGameObject.GetComponent<RectTransform>();
                        //rowRectTranform.anchoredPosition = new Vector3(0, -rowHeight * i, 0);
                        rowGameObject.gameObject.SetActive(true);

                        Debug.Log(rowGameObject.Find("Date"));
                        rowGameObject.Find("Date").GetComponent<TMP_Text>().text = ballGameData.getDate();
                        rowGameObject.Find("Range").GetComponent<TMP_Text>().text = ballGameData.getDist();
                        rowGameObject.Find("LeftHand").GetComponent<TMP_Text>().text = ballGameData.getLeftHand();
                        rowGameObject.Find("RightHand").GetComponent<TMP_Text>().text = ballGameData.getRightHand();
                        rowGameObject.Find("LeftScore").GetComponent<TMP_Text>().text = ballGameData.getLeftScore();
                        rowGameObject.Find("RightScore").GetComponent<TMP_Text>().text = ballGameData.getRightScore();
                        rowGameObject.Find("Time").GetComponent<TMP_Text>().text = ballGameData.getTime();
                    }
                }
            }

        }
    }
     
    public void ShowTableScoreBoardDrawGame(string id)
    {
        PatientData patientData = database.GetPatientDataById(id);
        if (patientData != null)
        {
            List<PatientData.DrawGameData> drawGameDatas = patientData.GetDrawGameDataList();
            if (drawGameDatas.Count == 0 || patientData.getName().Equals("Guest"))
            {
                Transform noContent = Instantiate(noContentText, containerDrawGame);
                instantiateList.Add(noContent);
            }
            else
            {
                for (int i = 0; i < drawGameDatas.Count; i++)
                {
                    Transform rowGameObject = Instantiate(rowScoreBoardDrawGame, containerDrawGame);
                    instantiateList.Add(rowGameObject);
                    RectTransform rowRectTranform = rowGameObject.GetComponent<RectTransform>();
                    //rowRectTranform.anchoredPosition = new Vector3(0, -rowHeight * i, 0);
                    rowGameObject.gameObject.SetActive(true);

                    PatientData.DrawGameData drawGameData = drawGameDatas[i];
                    Debug.Log(rowGameObject.Find("Date"));
                    rowGameObject.Find("Date").GetComponent<TMP_Text>().text = drawGameData.getDate();
                    rowGameObject.Find("Shape").GetComponent<TMP_Text>().text = drawGameData.getShape();
                    rowGameObject.Find("Level").GetComponent<TMP_Text>().text = drawGameData.getLevel();
                    rowGameObject.Find("Hand").GetComponent<TMP_Text>().text = drawGameData.getHand();
                    float totalTime = float.Parse(drawGameData.getTotalTime());
                    float errorTime = float.Parse(drawGameData.getErrorTime());
                    rowGameObject.Find("AccurateTime").GetComponent<TMP_Text>().text = totalTime.ToString();
                    rowGameObject.Find("ErrorTime").GetComponent<TMP_Text>().text = errorTime.ToString();
                    float accurateTime = 100f * (totalTime - errorTime) / totalTime;
                    rowGameObject.Find("AccuratePercent").GetComponent<TMP_Text>().text = accurateTime.ToString("0.00") + "%";
                }
            }

        }
    }

    public void RemoveInstanciate()
    {
        foreach (Transform t in instantiateList)
        {
            Destroy(t.gameObject);
        }
        instantiateList.Clear();
    }
}
