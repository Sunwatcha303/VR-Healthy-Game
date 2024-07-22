using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using System.Linq;

public class Database : MonoBehaviour
{
    private string csvUserFileName = "user.csv";
    private string csvPatientFileName = "patient.csv";
    private string csvBallGameFileName = "ballgame.csv";
    private string csvDrawGameFileName = "drawgame.csv";
    string filePathUser;
    string filePathPatient;
    string filePathBallGame;
    string filePathDrawGame;
    private Dictionary<string, string> user;
    private Dictionary<string, PatientData> patients;

    void Start()
    {
        // Combine the persistent data path and the file name
        filePathUser = Path.Combine(Application.dataPath, "VR-Healthy-Game", "Database", csvUserFileName);
        filePathPatient = Path.Combine(Application.dataPath, "VR-Healthy-Game", "Database", csvPatientFileName);
        filePathBallGame = Path.Combine(Application.dataPath, "VR-Healthy-Game", "Database", csvBallGameFileName);
        filePathDrawGame = Path.Combine(Application.dataPath, "VR-Healthy-Game", "Database", csvDrawGameFileName);
        // for build
        //string filePathUser = Path.Combine(Application.dataPath, csvUserFileName);
        //string filePathPatient = Path.Combine(Application.dataPath, csvPatientFileName);
        //string filePathBallGame = Path.Combine(Application.dataPath, csvBallGameFileName);
        //string filePathDrawGame = Path.Combine(Application.dataPath, csvDrawGameFileName);
        user = new Dictionary<string, string>();
        patients = new Dictionary<string, PatientData>();
        ReadCSVFileUser(filePathUser);
        ReadCSVFilePatient(filePathPatient,filePathBallGame,filePathDrawGame);
        Debug.Log(Application.dataPath);

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
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine("username,password");
                sw.WriteLine("admin,1234");
                sw.WriteLine("1234,admin");
            }
        }
    }

    void ReadCSVFilePatient(string filePath, string filePathBallGame, string filePathDrawGame)
    {
        if (File.Exists(filePath))
        {
            // Read the CSV file
            string[] lines = File.ReadAllLines(filePath);
            lines = lines.Skip(1).ToArray();
            foreach (string line in lines)
            {
                // Process each line as needed
                string[] input = line.Split(",");
                if (!patients.ContainsKey(input[0]))
                {
                    patients[input[0]] = new PatientData(input[0], input[1]);
                }
            }
        }
        else
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                // You can write content to the file if needed
                sw.WriteLine("id,name");
                sw.WriteLine("0001,Jack");
                sw.WriteLine("0002,John");
            }
        }
        if (File.Exists(filePathBallGame))
        {
            string[] linesBall = File.ReadAllLines(filePathBallGame);
            linesBall = linesBall.Skip(1).ToArray();
            foreach (string line in linesBall)
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
            using (StreamWriter sw = File.CreateText(filePathBallGame))
            {
                // You can write content to the file if needed
                sw.WriteLine("id,name,mode,scoreLeft,scoreRight,time,amount,isLeft,isRight,dist");
            }
        }

        if (File.Exists(filePathDrawGame))
        {
            string[] linesBall = File.ReadAllLines(filePathDrawGame);
            linesBall = linesBall.Skip(1).ToArray();
            foreach (string line in linesBall)
            {
                string[] input = line.Split(",");
                patients[input[0]].AddDrawGameData(input[2], input[3], input[4]);
            }
        }
        else
        {
            using (StreamWriter sw = File.CreateText(filePathDrawGame))
            {
                // You can write content to the file if needed
                sw.WriteLine("id,name,picture,accurate,time,time_out_the_box");
            }
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

    public void AddUser(string username, string password)
    {
        string[] data = { username, password };
        try
        {
            using (StreamWriter sw = new StreamWriter(filePathUser, true))
            {
                string line = string.Join(",", data);

                sw.WriteLine(line);
            }
            ReadCSVFileUser(filePathUser);

        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public void AddPatient(string name)
    {
        int id = patients.Count;
        string[] data = {""+(id+1), name};
        try
        {
            using (StreamWriter sw = new StreamWriter(filePathPatient, true))
            {
                string line = string.Join(",", data);

                sw.WriteLine(line);
            }
            ReadCSVFilePatient(filePathPatient, filePathBallGame, filePathDrawGame);

        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }
    }

    public string GetNameByIdOrName(string name)
    {
        foreach(var p in patients)
        {
            if (p.Value.getName().Equals(name))
            {
                return p.Value.getName();
            }
        }
        return null;
    }

    internal string GetIdByName(string name)
    {
        foreach (var p in patients)
        {
            if (p.Value.getName().Equals(name))
            {
                return p.Key;
            }
        }
        return null;
    }
}
