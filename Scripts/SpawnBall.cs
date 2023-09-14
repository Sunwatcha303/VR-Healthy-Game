using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public GameObject BallForLeft;
    public GameObject BallForRight;
    public bool ready = true;
    public bool isQSystem = false;
    float spawnTime = 0.0f;
    public float near = 0.5f;
    public float normal = 1f;
    public float far = 1.5f;
    float posZ;
    public GameObject[] table;
    Queue<SelectBallLocation> q;
    List<GameObject> listBallInstance = new List<GameObject>();
    System.Random random = new System.Random();

    // Update is called once per frame
    void Start(){
        posZ = near;
    }
    void Update()
    {
        if (ready && (Time.time > spawnTime))
        {
            if(isQSystem){
                GameObject temp;
                SelectBallLocation selection = q.Dequeue();
                if(random.Next(0, 2) == 1){
                    temp = (GameObject)Instantiate(BallForLeft);
                    listBallInstance.Add(temp);
                }
                else{
                    temp = (GameObject)Instantiate(BallForRight);
                    listBallInstance.Add(temp);
                }
                Vector3 spawnPosition = new Vector3(selection.getX(), selection.getY(),posZ);
                temp.transform.position = spawnPosition;
            }
            else{
                foreach(SelectBallLocation selection in q){
                    GameObject temp;
                    if(random.Next(0, 2) == 1){
                        temp = (GameObject)Instantiate(BallForLeft);
                        listBallInstance.Add(temp);
                    }
                    else{
                        temp = (GameObject)Instantiate(BallForRight);
                        listBallInstance.Add(temp);
                    }
                    Vector3 spawnPosition = new Vector3(selection.getX(), selection.getY(),posZ);
                    temp.transform.position = spawnPosition;
                }
            }
            ready = false;
        }
    }
    public void setReady()
    {
        ready = true;
        spawnTime = Time.time + 0.5f;
    }

    public void setQueueBall(){
        q = new Queue<SelectBallLocation>();
        for(int i=0;i<table.Length;i++){
            SelectBallLocation selection = table[i].GetComponent<SelectBallLocation>();
            if (selection.getSelect()){
                q.Enqueue(selection);
            }
        }
    }

    public void setPosZ(string str){
        switch(str){
            case "near":
                posZ = near;
                break;
            case "normal":
                posZ = normal;
                break;
            case "far":
                posZ = far;
                break;
        }
    }

    public bool getQSystem(){
        return isQSystem;
    }

    public void setQSystem(){
        if(isQSystem){
            isQSystem = false;
        }
        else{
            isQSystem = true;
        }
    }

    public void DestroyBall(){
        foreach(GameObject ball in listBallInstance){
            Destroy(ball);
        }
        listBallInstance.Clear();
    }
}
