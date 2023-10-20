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

    public void SaveToLog(int score, float time, int amount, bool isLeft, bool isRight, string dist)
    {
        string filePath = Application.dataPath + "/VR-Healthy-Game/Log/data.csv";

        string[] dataToAppend = { "" + score, "" + time, "" + amount, "" + isLeft, "" + isRight, dist }; // Example data

        AppendToCsv(filePath, dataToAppend);
    }
}
