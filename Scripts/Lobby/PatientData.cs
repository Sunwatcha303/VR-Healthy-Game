using System;
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
        private string mode;
        private string rightScore;
        private string leftScore;
        private string time;
        private string isLeft;
        private string isRight;
        private string dist;
        private string date;

        public BallGameData(string mode, string rightScore, string leftScore, string time, string isLeft, string isRight, string dist, string date)
        {
            this.mode = mode;
            this.rightScore = rightScore;
            this.leftScore = leftScore;
            this.time = time;
            this.isLeft = isLeft;
            this.isRight = isRight;
            this.dist = dist;
            this.date = date;
        }

        internal string getTime()
        {
            return time;
        }

        internal string getDist()
        {
            return dist;
        }

        internal string getLeftHand()
        {
            return isLeft.Equals("True") ? "Yes" : "No";
        }

        internal string getLeftScore()
        {
            return leftScore;
        }

        internal string getRightHand()
        {
            return isRight.Equals("True") ? "Yes" : "No";
        }

        internal string getRightScore()
        {
            return rightScore;
        }

        internal string getDate()
        {
            return date;
        }

        internal string getMode()
        {
            return mode;
        }
    }

    public class DrawGameData
    {
        private string shape;
        private string level;
        private string totalTime;
        private string errorTime;
        private string hand;
        private string date;

        public DrawGameData(string shape, string level, string totalTime, string errorTime, string hand, string date)
        {
            this.shape = shape;
            this.level = level;
            this.totalTime = totalTime;
            this.errorTime = errorTime;
            this.hand = hand;
            this.date = date;
        }

        internal string getDate()
        {
            return date;
        }

        internal string getErrorTime()
        {
            return errorTime;
        }

        internal string getLevel()
        {
            return level.Equals("0") ? "Easy" : (level.Equals("1") ? "Normal" : "Hard");
        }

        internal string getHand()
        {
            return hand.Equals("R") ? "Right" : "Left";
        }

        internal string getShape()
        {
            return shape.Equals("0") ? "Circle" : (shape.Equals("1") ? "Square" : "Triangle");
        }

        internal string getTotalTime()
        {
            return totalTime;
        }
    }

    public PatientData(string id, string name) 
    {
        this.id = id;
        this.name = name;
        ballGameData = new List<BallGameData>();
        drawGameData = new List<DrawGameData>();
    }

    public void AddBallGameData(string mode, string rightScore, string leftScore, string time, string isLeft, string isRight, string dist, string date)
    {
        ballGameData.Add(new BallGameData( mode, rightScore, leftScore,  time,  isLeft,  isRight, dist, date));
    }

    public void AddDrawGameData(string shape, string level, string totaltime, string errorTime, string hand, string date)
    {
        drawGameData.Add(new DrawGameData(shape, level, totaltime, errorTime, hand, date));
    }

    public string getName()
    {
        return name;
    }

    public List<BallGameData> GetBallGameDataList()
    {
        return ballGameData;
    }
    public List<DrawGameData> GetDrawGameDataList()
    {
        return drawGameData;
    }
}