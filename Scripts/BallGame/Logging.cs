using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class Logging : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AppendToCsv(string filePath, string[] data)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                string line = string.Join(",", data);

                sw.WriteLine(line);
            }

        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void SaveToLog(string mode, int scoreLeft, int scoreRight, float time, int amount, bool isLeft, bool isRight, string dist)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "ballgame.csv");

        string[] dataToAppend = { PlayerPrefs.GetString("currentId"), PlayerPrefs.GetString("currentName"), "" + mode, "" + scoreLeft, "" + scoreRight, "" + time, "" + amount, "" + isLeft, "" + isRight, dist };

        AppendToCsv(filePath, dataToAppend);
    }
}
