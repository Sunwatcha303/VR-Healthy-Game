using System.Collections.Generic;
using UnityEngine;
public class PatientData
{
    string id;
    string name;
    List<BallGameData> ballGameData;
    List<DrawGameData> drawGameData;
    public class BallGameData
    {
        private string score;
        private string time;
        private string amount;
        private string isLeft;
        private string isRight;
        private string dist;

        public BallGameData(string score, string time, string amount, string isLeft, string isRight, string dist)
        {
            this.score = score;
            this.time = time;
            this.amount = amount;
            this.isLeft = isLeft;
            this.isRight = isRight;
            this.dist = dist;
        }
    }

    public class DrawGameData
    {
        private string picture;
        private string accurate;
        private string time;

        public DrawGameData(string picture, string accurate, string time)
        {
            this.picture = picture;
            this.accurate = accurate;
            this.time = time;
        }
    }

    public PatientData(string id, string name)
    {
        this.id = id;
        this.name = name;
        ballGameData = new List<BallGameData>();
        drawGameData = new List<DrawGameData>();
    }

    public void AddBallGameData(string score, string time, string amount, string isLeft, string isRight, string dist)
    {
        ballGameData.Add(new BallGameData(score, time, amount, isLeft, isRight, dist));
    }

    public void AddDrawGameData(string picture, string accurate, string time)
    {
        drawGameData.Add(new DrawGameData(picture, accurate, time));
    }

    public string getName()
    {
        return name;
    }
}