using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Database : MonoBehaviour
{
    private string csvFileName = "user.csv";
    private Dictionary<string, string> user;

    void Start()
    {
        // Combine the persistent data path and the file name
        string filePath = Path.Combine(Application.persistentDataPath, csvFileName);
        user = new Dictionary<string, string>();
        ReadCSVFile(filePath);
        Debug.Log(Application.persistentDataPath);

    }

    void ReadCSVFile(string filePath)
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
}
