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

    public void SaveToLog(string mode, int scoreLeft, int scoreRight, float time, bool isLeft, bool isRight, string dist)
    {
        //string path = Path.Combine(Application.dataPath, "VR-Healthy-Game", "Database" , "ballgame.csv");
        //for build
        string path = Path.Combine(Application.dataPath, "ballgame.csv");
        Debug.Log(path);

        string[] dataToAppend = { PlayerPrefs.GetString("currentId", "0000"), "" + mode, "" + scoreLeft, "" + scoreRight, time.ToString("0.00"), "" + isLeft, "" + isRight, dist, ""+DateTime.Today.ToString("dd/MM/yyyy") };

        AppendToCsv(path, dataToAppend);
    }
}
