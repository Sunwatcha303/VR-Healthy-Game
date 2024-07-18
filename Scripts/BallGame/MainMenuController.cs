using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mainMenu;
    public GameObject setModeMenu;
    public GameObject mainCamera;
    public GameObject freeMode;
    public GameController controller;
    public SpawnBall spawnBall;
    public SelectDistance selectDistanceSETmode;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (controller.getStart() && Input.GetKeyDown(KeyCode.P))
        {
            // Debug.Log("test");
            resetMenu();
        }
    }

    public void resetMenu()
    {
        spawnBall.DestroyBall();
        spawnBall.SetFreeMode(false);
        spawnBall.setQSystem(false);
        spawnBall.setPosZ("near");

        selectDistanceSETmode.SetDropdownToDefault();


        controller.setFinish();
        mainMenu.SetActive(true);
        setModeMenu.SetActive(true);
        freeMode.SetActive(false);
        //mainCamera.SetActive(false);
    }
}
