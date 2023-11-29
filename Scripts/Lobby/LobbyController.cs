using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LobbyController : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btnBallGame;
    public Button btnDrawGame;
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField idInput;
    public Toggle toggle;
    public GameObject loginScene;
    public GameObject menuScene;
    public GameObject selectScene;
    public Database database;

    void Start()
    {
        btnBallGame.GetComponent<Button>().onClick.AddListener(LoadSceneBallGame);
        btnDrawGame.GetComponent<Button>().onClick.AddListener(LoadSceneDrawGame);
        if (PlayerPrefs.GetInt("IsLoggedIn", 0) == 0)
        {
            loginScene.SetActive(true);
            selectScene.SetActive(false);
        }
        else
        {
            loginScene.SetActive(false);
            selectScene.SetActive(true);
        }
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

    public void Login()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;
        if (password == database.GetPasswordByUsername(username))
        {
            loginScene.SetActive(false);
            selectScene.SetActive(true);
            usernameInput.text = "";
            passwordInput.text = "";
            PlayerPrefs.SetInt("IsLoggedIn", 1);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("failed");
        }
    }

    public void LogOut()
    {
        PlayerPrefs.SetInt("IsLoggedIn", 0);
        PlayerPrefs.Save();
        loginScene.SetActive(true);
        selectScene.SetActive(false);
    }

    public void ShowPassword()
    {
        if (toggle.isOn)
        {
            passwordInput.contentType = TMP_InputField.ContentType.Standard;
            passwordInput.inputType = TMP_InputField.InputType.Standard;
        }
        else
        {
            passwordInput.contentType = TMP_InputField.ContentType.Password;
            passwordInput.inputType = TMP_InputField.InputType.Password;
        }
        passwordInput.ForceLabelUpdate();
    }

    public void SelectPatient()
    {
        string id = idInput.text;
        if(database.GetNameById(id) != null)
        {
            selectScene.SetActive(false);
            menuScene.SetActive(true);
            PlayerPrefs.SetString("currentId", id);
            PlayerPrefs.SetString("currentName", database.GetNameById(id));
            PlayerPrefs.Save();
            idInput.text = "";
        }
    }

    public void Back()
    {
        menuScene.SetActive(false);
        selectScene.SetActive(true);
        PlayerPrefs.DeleteKey("currentId");
        PlayerPrefs.DeleteKey("currentName");
        PlayerPrefs.Save();
    }
}
