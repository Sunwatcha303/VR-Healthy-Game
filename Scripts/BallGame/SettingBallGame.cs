using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SettingBallGame : MonoBehaviour
{
    private string filePath;
    private Dictionary<string, float> settingsDict = new Dictionary<string, float>();

    public SpawnBall spawnBall;
    public TMP_InputField armLenText;
    public TMP_InputField nearDistText;
    public TMP_InputField normalDistText;
    public TMP_InputField farDistText;
    public TMP_InputField ballXPos;
    public TMP_InputField ballYPos;


    // Start is called before the first frame update
    void Start()
    {
        //filePath = Path.Combine(Application.dataPath, "VR-Healthy-Game", "Database", "Setting.txt");
        // for build
        filePath = Path.Combine(Application.dataPath, "Setting.txt");
        LoadSetting();
    }

    public void LoadSetting()
    {
        if (!File.Exists(filePath))
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine("Length_of_Arm: 0.67");
                sw.WriteLine("Distance_of_near_range: 0.6");
                sw.WriteLine("Distance_of_normal_range: 0.8");
                sw.WriteLine("Distance_of_far_range: 1");
                sw.WriteLine("Ball_game_x_position: -0.75");
                sw.WriteLine("Ball_game_y_position: 1.75");
                sw.WriteLine("Draw_game_x_position: 0");
                sw.WriteLine("Draw_game_y_position: 5");
                sw.WriteLine("Draw_game_z_position: 7");
                sw.WriteLine("Ray_draw_distance: 10");

            }
        }

        // Read the CSV file
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            // Process each line as needed
            string[] input = line.Split(": ");
            //Debug.Log(input[0] + input[1]);
            settingsDict[input[0]] = float.Parse(input[1]);
        }
        //Debug.Log(settingsDict.Count);

        SetPage();
    }

    private void SetPage()
    {
        float armLen = settingsDict["Length_of_Arm"];
        float nearLen = settingsDict["Distance_of_near_range"];
        float normalLen = settingsDict["Distance_of_normal_range"];
        float farLen = settingsDict["Distance_of_far_range"];
        float xPos = settingsDict["Ball_game_x_position"];
        float yPos = settingsDict["Ball_game_y_position"];

        spawnBall.setArmLen(armLen);
        spawnBall.setNearDist(nearLen);
        spawnBall.setNormalDist(normalLen);
        spawnBall.setFarDist(farLen);

        armLenText.text = armLen.ToString();
        nearDistText.text = nearLen.ToString();
        normalDistText.text = normalLen.ToString();
        farDistText.text = farLen.ToString();
        ballXPos.text = xPos.ToString();
        ballYPos.text = yPos.ToString();
    }

    public void ApplyChangeSetting()
    {
        float armLen = float.Parse(armLenText.text);
        float nearLen = float.Parse(nearDistText.text);
        float normalLen = float.Parse(normalDistText.text);
        float farLen = float.Parse(farDistText.text);
        float xPos = float.Parse(ballXPos.text);
        float yPos = float.Parse(ballYPos.text);

        settingsDict["Length_of_Arm"] = armLen;
        settingsDict["Distance_of_near_range"] = nearLen;
        settingsDict["Distance_of_normal_range"] = normalLen;
        settingsDict["Distance_of_far_range"] = farLen;
        settingsDict["Ball_game_x_position"] = xPos;
        settingsDict["Ball_game_y_position"] = yPos;

        using (StreamWriter sw = File.CreateText(filePath))
        {
            sw.WriteLine("Length_of_Arm: " + armLen.ToString());
            sw.WriteLine("Distance_of_near_range: " + nearLen.ToString());
            sw.WriteLine("Distance_of_normal_range: " + normalLen.ToString());
            sw.WriteLine("Distance_of_far_range: " + farLen.ToString());
            sw.WriteLine("Ball_game_x_position: " + xPos.ToString());
            sw.WriteLine("Ball_game_y_position: " + yPos.ToString());
            sw.WriteLine("Draw_game_x_position: " + settingsDict["Draw_game_x_position"].ToString());
            sw.WriteLine("Draw_game_y_position: " + settingsDict["Draw_game_y_position"].ToString());
            sw.WriteLine("Draw_game_z_position: " + settingsDict["Draw_game_z_position"].ToString());
            sw.WriteLine("Ray_draw_distance: " + settingsDict["Ray_draw_distance"].ToString());
        }

        SetPage();
    }
}
