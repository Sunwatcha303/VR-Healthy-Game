using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class LobbyController : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btnBallGame;
    public Button btnDrawGame;
    public TMP_InputField usernameInputLogin;
    public TMP_InputField passwordInputLogin;
    public TMP_InputField usernameInputAdd;
    public TMP_InputField passwordInputAdd;
    public TMP_InputField idInput;
    public Toggle toggleLogin;
    public Toggle toggleAdd;
    public GameObject loginScene;
    public GameObject menuScene;
    public GameObject selectScene;
    public Database database;

    void Start()
    {
        btnBallGame.GetComponent<Button>().onClick.AddListener(LoadSceneBallGame);
        btnDrawGame.GetComponent<Button>().onClick.AddListener(LoadSceneDrawGame);
        //loginScene.SetActive(false);
        //PlayerPrefs.DeleteKey("currentName");
        //PlayerPrefs.DeleteKey("currentId");

        /*if(PlayerPrefs.GetInt("IsLoggedIn",0) == 0)
        {
            loginScene.SetActive(true);
            selectScene.SetActive(false);
            menuScene.SetActive(false);
        }*/
        if (PlayerPrefs.GetInt("IsLoggedIn") == 1)
        {
            if (PlayerPrefs.GetString("currentName", "0") == "0")
            {
                menuScene.SetActive(false);
                selectScene.SetActive(true);
            }
            else
            {
                selectScene.SetActive(false);
                menuScene.SetActive(true);
            }
        }
        else
        {
            loginScene.SetActive(true);
            selectScene.SetActive(false);
            menuScene.SetActive(false);
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
        string username = usernameInputLogin.text;
        string password = passwordInputLogin.text;
        if (password == database.GetPasswordByUsername(username))
        {
            loginScene.SetActive(false);
            selectScene.SetActive(true);
            usernameInputLogin.text = "";
            passwordInputLogin.text = "";
            PlayerPrefs.SetInt("IsLoggedIn", 1);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("failed");
        }
    }

    public void AddUser()
    {
        string username = usernameInputAdd.text;
        string password = passwordInputAdd.text;
        
        if(database.GetPasswordByUsername(username) == null)
        {
            database.AddUser(username, password);
        }
        else
        {
            Debug.Log(database.GetPasswordByUsername(username));
        }
    }

    public void LogOut()
    {
        PlayerPrefs.SetInt("IsLoggedIn", 0);
        PlayerPrefs.Save();
        loginScene.SetActive(true);
        selectScene.SetActive(false);
    }

    public void ShowPasswordLgoin()
    {
        if (toggleLogin.isOn)
        {
            passwordInputLogin.contentType = TMP_InputField.ContentType.Standard;
            passwordInputLogin.inputType = TMP_InputField.InputType.Standard;
        }
        else
        {
            passwordInputLogin.contentType = TMP_InputField.ContentType.Password;
            passwordInputLogin.inputType = TMP_InputField.InputType.Password;
        }
        passwordInputLogin.ForceLabelUpdate();
    }
    public void ShowPasswordAdd()
    {
        if (toggleAdd.isOn)
        {
            passwordInputAdd.contentType = TMP_InputField.ContentType.Standard;
            passwordInputAdd.inputType = TMP_InputField.InputType.Standard;
        }
        else
        {
            passwordInputAdd.contentType = TMP_InputField.ContentType.Password;
            passwordInputAdd.inputType = TMP_InputField.InputType.Password;
        }
        passwordInputAdd.ForceLabelUpdate();
    }
    public void SelectPatient()
    {
        string name = idInput.text;
        //Debug.Log(name);
        if (!name.Equals(""))
        {
            Debug.Log(database.GetNameByIdOrName(name));
            if(database.GetNameByIdOrName(name) == null)
            {
                database.AddPatient(name);
            }
            selectScene.SetActive(false);
            menuScene.SetActive(true);
            PlayerPrefs.SetString("currentName", name);
            PlayerPrefs.SetString("currentId", database.GetIdByName(name));
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

    public void ContunueWithGuest()
    {
        selectScene.SetActive(false);
        menuScene.SetActive(true);
        System.Random rand = new System.Random();
        int id = rand.Next(100000, 999999);
        PlayerPrefs.SetString("currentName", "Guest");
        PlayerPrefs.SetString("currentId", ""+id);
        PlayerPrefs.Save();
    }
}
