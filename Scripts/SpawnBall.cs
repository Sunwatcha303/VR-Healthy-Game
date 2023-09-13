using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public GameObject objToSpawn;
    Vector3 previousSpawnPosition;
    public bool ready = true;
    float spawnTime = 0.0f;
    public float near = 0.5f;
    public float normal = 1f;
    public float far = 1.5f;
    float posZ;
    public GameObject[] table;
    Queue<SelectBallLocation> q;

    // Update is called once per frame
    void Start(){
        posZ = near;
    }
    void Update()
    {
        if (ready && (Time.time > spawnTime))
        {
            foreach(SelectBallLocation selection in q){
                GameObject temp;
                temp = (GameObject)Instantiate(objToSpawn);
                Vector3 spawnPosition = new Vector3(selection.getX(), selection.getY(),posZ);
                temp.transform.position = spawnPosition;
                previousSpawnPosition = spawnPosition;
            }
            // SelectBallLocation selection = q.Dequeue();
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
}
