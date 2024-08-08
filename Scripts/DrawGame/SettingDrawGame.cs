using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SettingDrawGame : MonoBehaviour
{
    private string filePath;
    private Dictionary<string, float> settingsDict = new Dictionary<string, float>();

    public TMP_InputField xPosFeild;
    public TMP_InputField yPosFeild;
    public TMP_InputField zPosFeild;

    public TMP_InputField rayDistField;

    public DrawGameController drawGameController;

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
                sw.WriteLine("Draw_game_y_position: 2");
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
        Debug.Log(settingsDict.Count);

        SetPage();
    }

    private void SetPage()
    {
        float xPos = settingsDict["Draw_game_x_position"];
        float yPos = settingsDict["Draw_game_y_position"];
        float zPos = settingsDict["Draw_game_z_position"];
        float rayDist = settingsDict["Ray_draw_distance"];

        drawGameController.setPosBoard(xPos, yPos, zPos);
        drawGameController.setRayDistance(rayDist);
;
        xPosFeild.text = xPos.ToString();
        yPosFeild.text = yPos.ToString();
        zPosFeild.text = zPos.ToString();
        rayDistField.text = rayDist.ToString();
    }

    public void ApplyChangeSetting()
    {
        float xPos = float.Parse(xPosFeild.text);
        float yPos = float.Parse(yPosFeild.text);
        float zPos = float.Parse(zPosFeild.text);
        float rayDist = float.Parse(rayDistField.text);

        settingsDict["Draw_game_x_position"] = xPos;
        settingsDict["Draw_game_y_position"] = yPos;
        settingsDict["Draw_game_z_position"] = zPos;
        settingsDict["Ray_draw_distance"] = rayDist;

        using (StreamWriter sw = File.CreateText(filePath))
        {
            sw.WriteLine("Length_of_Arm: " + settingsDict["Length_of_Arm"].ToString());
            sw.WriteLine("Distance_of_near_range: " + settingsDict["Distance_of_near_range"].ToString());
            sw.WriteLine("Distance_of_normal_range: " + settingsDict["Distance_of_normal_range"].ToString());
            sw.WriteLine("Distance_of_far_range: " + settingsDict["Distance_of_far_range"].ToString());
            sw.WriteLine("Ball_game_x_position: " + settingsDict["Ball_game_x_position"].ToString());
            sw.WriteLine("Ball_game_y_position: " + settingsDict["Ball_game_y_position"].ToString());
            sw.WriteLine("Draw_game_x_position: " + xPos.ToString());
            sw.WriteLine("Draw_game_y_position: " + yPos.ToString());
            sw.WriteLine("Draw_game_z_position: " + zPos.ToString());
            sw.WriteLine("Ray_draw_distance: " + rayDist.ToString());
        }

        SetPage();
    }
}
