using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Database : MonoBehaviour
{
    private string csvUserFileName = "user.csv";
    private string csvPatientFileName = "patient.csv";
    private string csvBallGameFileName = "ballgame.csv";
    private string csvDrawGameFileName = "drawgame.csv";
    private Dictionary<string, string> user;
    private Dictionary<string, PatientData> patients;

    void Start()
    {
        // Combine the persistent data path and the file name
        string filePathUser = Path.Combine(Application.persistentDataPath, csvUserFileName);
        string filePathPatient = Path.Combine(Application.persistentDataPath, csvPatientFileName);
        string filePathBallGame = Path.Combine(Application.persistentDataPath, csvBallGameFileName);
        string filePathDrawGame = Path.Combine(Application.persistentDataPath, csvDrawGameFileName);
        user = new Dictionary<string, string>();
        patients = new Dictionary<string, PatientData>();
        ReadCSVFileUser(filePathUser);
        ReadCSVFilePatient(filePathPatient,filePathBallGame,filePathDrawGame);
        Debug.Log(Application.persistentDataPath);

    }

    void ReadCSVFileUser(string filePath)
    {
        if (File.Exists(filePath))
        {
            // Read the CSV file
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                // Process each line as needed
                string[] input = line.Split(",");
                user[input[0]] = input[1];
            }
        }
        else
        {
            Debug.LogError("CSV file not found!");
        }
    }

    void ReadCSVFilePatient(string filePath, string filePathBallGame, string filePathDrawGame)
    {
        if (File.Exists(filePath))
        {
            // Read the CSV file
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                // Process each line as needed
                string[] input = line.Split(",");
                if (!patients.ContainsKey(input[0]))
                {
                    patients[input[0]] = new PatientData(input[0], input[1]);
                }
            }

            if (File.Exists(filePathBallGame))
            {
                string[] linesBall = File.ReadAllLines(filePathBallGame);
                foreach(string line in linesBall)
                {
                    string[] input = line.Split(",");
                    if (input[0] != String.Empty && !patients.ContainsKey(input[0]))
                    {
                        patients[input[0]] = new PatientData(input[0], input[1]);
                    }
                    patients[input[0]].AddBallGameData(input[2], input[3], input[4], input[5], input[6], input[7]);
                }
            }
            else
            {
                Debug.LogError("CSV file not found! " + filePathBallGame);
            }

            if (File.Exists(filePathDrawGame))
            {
                string[] linesBall = File.ReadAllLines(filePathDrawGame);
                foreach (string line in linesBall)
                {
                    string[] input = line.Split(",");
                    patients[input[0]].AddDrawGameData(input[2], input[3], input[4]);
                }
            }
            else
            {
                Debug.LogError("CSV file not found! " + filePathDrawGame);
            }
        }
        else
        {
            Debug.LogError("CSV file not found! "+filePath);
        }
    }

    void WriteCSVFile(string filePath)
    {
        // Example: Write some data to the CSV file
        string[] data = { "username,password", "admin123,1234", "admin321,4321" };

        // Write the data to the CSV file
        File.WriteAllLines(filePath, data);
    }

    public string GetPasswordByUsername(string username)
    {
        if (user.ContainsKey(username))
        {
            return user[username];
        }
        return null;
    }

    public string GetNameById(string id)
    {
        if (patients.ContainsKey(id))
        {
            return patients[id].getName();
        }
        return null;
    }
}
