using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btnBallGame;
    public Button btnDrawGame;
    void Start()
    {
        btnBallGame.GetComponent<Button>().onClick.AddListener(LoadSceneBallGame);
        btnDrawGame.GetComponent<Button>().onClick.AddListener(LoadSceneDrawGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadSceneBallGame()
    {
        SceneManager.LoadScene("BallGame");
    }

    void LoadSceneDrawGame()
    {
        SceneManager.LoadScene("DrawGame");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
