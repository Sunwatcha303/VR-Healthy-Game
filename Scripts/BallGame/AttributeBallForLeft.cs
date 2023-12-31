using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttributeBallForLeft : MonoBehaviour
{
    public float speed = 100f;
    GameController gameController;
    SpawnBall spawner;
    void Start()
    {
        if (gameController == null)
        {
            GameObject _gController = GameObject.FindGameObjectWithTag("GameController") as GameObject;
            gameController = _gController.GetComponent<GameController>();
        }
        if (spawner == null)
        {
            GameObject spawnball = GameObject.FindGameObjectWithTag("Spawner") as GameObject;
            spawner = spawnball.GetComponent<SpawnBall>();
        }
    }

    void Update()
    {
        transform.Rotate(speed * Time.deltaTime / 10, speed * Time.deltaTime * 2, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.tag.Equals("LeftHand"))
        {
            Destroy(this.gameObject);
            gameController.setScoreLeft();
            spawner.RemoveBall(this.gameObject);
            if (spawner.getQSystem())
            {
                spawner.setReady();
            }
        }
    }
}
