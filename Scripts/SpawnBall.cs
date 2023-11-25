using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnBall : MonoBehaviour
{
    public GameObject BallForLeft;
    public GameObject BallForRight;
    public bool ready = true;
    public bool isQSystem = false;
    float spawnTime = 0.0f;
    public bool isNear = true;
    public bool isNormal = false;
    public bool isFar = false;
    float posZ = 0;
    public GameObject posPlayer;
    public GameObject[] table;
    Queue<SelectBallLocation> q;
    List<GameObject> listBallInstance = new List<GameObject>();
    System.Random random = new System.Random();
    int count = 0;

    bool isLeftHand = false;
    bool isRightHand = false;
    bool isOneHand = false;
    public GameController gameController;

    // Update is called once per frame
    void Start()
    {
        isNear = true;
    }
    void Update()
    {
        if (q.Count == 0 && listBallInstance.Count == 0)
        {
            EndGame();
        }
        if (ready && (Time.time > spawnTime))
        {
            if (isQSystem)
            {
                GameObject temp;
                ShuffleQueue<SelectBallLocation>(q);
                SelectBallLocation selection = q.Dequeue();
                if (isOneHand)
                {
                    if (isLeftHand)
                    {
                        temp = (GameObject)Instantiate(BallForLeft);
                        listBallInstance.Add(temp);
                    }
                    else
                    {
                        temp = (GameObject)Instantiate(BallForRight);
                        listBallInstance.Add(temp);
                    }
                }
                else
                {
                    if (random.Next(0, 2) == 1)
                    {
                        temp = (GameObject)Instantiate(BallForLeft);
                        listBallInstance.Add(temp);
                    }
                    else
                    {
                        temp = (GameObject)Instantiate(BallForRight);
                        listBallInstance.Add(temp);
                    }
                }
                if (isNear)
                {
                    posZ = 0.5f;
                }
                else if (isNormal)
                {
                    posZ = 0.7f;
                }
                else
                {
                    posZ = 0.9f;
                }
                Debug.Log(posZ);
                Vector3 spawnPosition = new Vector3(selection.getX(), selection.getY(), posZ);
                temp.transform.position = spawnPosition;
            }
            else
            {
                foreach (SelectBallLocation selection in q)
                {
                    GameObject temp;
                    if (isOneHand)
                    {
                        if (isLeftHand)
                        {
                            temp = (GameObject)Instantiate(BallForLeft);
                            listBallInstance.Add(temp);
                        }
                        else
                        {
                            temp = (GameObject)Instantiate(BallForRight);
                            listBallInstance.Add(temp);
                        }
                    }
                    else
                    {
                        if (random.Next(0, 2) == 1)
                        {
                            temp = (GameObject)Instantiate(BallForLeft);
                            listBallInstance.Add(temp);
                        }
                        else
                        {
                            temp = (GameObject)Instantiate(BallForRight);
                            listBallInstance.Add(temp);
                        }
                    }
                    if (isNear)
                    {
                        posZ = 0.5f;
                    }
                    else if (isNormal)
                    {
                        posZ = 0.7f;
                    }
                    else
                    {
                        posZ = 0.9f;
                    }
                    Debug.Log(posZ);
                    Vector3 spawnPosition = new Vector3(selection.getX(), selection.getY(), posZ);
                    temp.transform.position = spawnPosition;
                }
                q.Clear();
            }
            ready = false;
        }

    }

    public void EndGame()
    {
        gameController.setEndGame();
    }
    public void setReady()
    {
        ready = true;
        spawnTime = Time.time + 0.5f;
        Debug.Log(isRightHand);
        Debug.Log(isLeftHand);
        Debug.Log(isOneHand);
    }

    public void setQueueBall()
    {
        q = new Queue<SelectBallLocation>();
        for (int i = 0; i < table.Length; i++)
        {
            SelectBallLocation selection = table[i].GetComponent<SelectBallLocation>();
            if (selection.getSelect())
            {
                q.Enqueue(selection);
            }
        }
    }

    public void setPosZ(string str)
    {
        switch (str)
        {
            case "near":
                isNear = true;
                isNormal = false;
                isFar = false;
                break;
            case "normal":
                isNormal = true;
                isNear = false;
                break;
            case "far":
                isFar = true;
                isNear = false;
                isNormal = false;
                break;
        }
    }

    public bool getQSystem()
    {
        return isQSystem;
    }

    public void setQSystem()
    {
        if (isQSystem)
        {
            isQSystem = false;
        }
        else
        {
            isQSystem = true;
        }
    }

    public void DestroyBall()
    {
        count = listBallInstance.Count;
        foreach (GameObject ball in listBallInstance)
        {
            Destroy(ball);
        }
        listBallInstance.Clear();
    }
    public void setIsOneHand(bool b)
    {
        isOneHand = b;
    }
    public void setIsLeftHand(bool b)
    {
        isLeftHand = b;
        Debug.Log(isLeftHand);
        isOneHand = (isLeftHand && isRightHand) ? false : true;
    }
    public void setIsRightHand(bool b)
    {
        isRightHand = b;
        Debug.Log(isRightHand);
        isOneHand = (isLeftHand && isRightHand) ? false : true;
    }
    public void resetForBackButton()
    {
        setIsOneHand(false);
        setIsLeftHand(false);
        setIsRightHand(false);
    }

    public int getSizeListBall()
    {
        return count;
    }

    public string getDistance()
    {
        if (isNear) return "near";
        else if (isNormal) return "normal";
        else return "far";
    }

    private void ShuffleQueue<T>(Queue<T> queue)
    {
        // Convert the Queue to a List
        List<T> list = new List<T>(queue);

        // Use Fisher-Yates shuffle algorithm to shuffle the list
        System.Random rand = new System.Random();
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        // Convert the shuffled List back to a Queue
        queue.Clear();
        foreach (var item in list)
        {
            queue.Enqueue(item);
        }
    }
    public void RemoveBall(GameObject g)
    {
        listBallInstance.Remove(g);
    }
}

