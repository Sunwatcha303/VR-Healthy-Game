using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mainMenu;
    public GameObject setModeMenu;
    public GameObject mainCamera;
    public GameController controller;
    public SpawnBall spawnBall;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (controller.getStart() && Input.GetKeyDown(KeyCode.P))
        {
            // Debug.Log("test");
            spawnBall.DestroyBall();
            spawnBall.SetFreeMode(false);
            spawnBall.setQSystem(false);
            controller.setFinish();
            mainMenu.SetActive(true);
            setModeMenu.SetActive(true);
            mainCamera.SetActive(false);
        }
    }
}
