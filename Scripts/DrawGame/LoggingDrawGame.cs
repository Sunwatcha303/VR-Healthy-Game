using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoggingDrawGame : MonoBehaviour
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

    public void SaveToLog(int picture, double accurate, double time)
    {
        string path = Path.Combine(Application.dataPath, "VR-Healthy-Game", "Database" ,"drawgame.csv");
        //for build
        //string path = Path.Combine(Application.dataPath ,"drawgame.csv");
        Debug.Log(path);

        string[] dataToAppend = { PlayerPrefs.GetString("currentId"), PlayerPrefs.GetString("currentName"), "" + picture, accurate.ToString("F2"), "" + time.ToString("F2")};

        AppendToCsv(path, dataToAppend);
    }
}
