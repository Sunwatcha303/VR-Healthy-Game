using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject selectModeMenu;
    public GameObject pauseButton;
    public GameController gameController;
    public SpawnBall spawnBall;
    void Start()
    {
        pauseButton.GetComponent<Button>().onClick.AddListener(resetMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void resetMenu()
    {
        spawnBall.DestroyBall();

        gameController.setFinish();
        selectModeMenu.SetActive(true);

        if (!spawnBall.GetIsFreeMode())
        {
            gameController.SetActiveSetAndRandMode(true);
        }
        else
        {
            gameController.SetActiveSetAndRandMode(false);
        }
    }
}
