using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private void Start()
    {
        Debug.Log(Input.GetJoystickNames());
    }

    public void StartNewGame()
    {
        PlayerPrefs.SetInt("CP", 0);
        PlayerPrefs.SetInt("Scene", 1);
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Scene"));
    }

    private void OpenOptions()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void ConfirmExitGame()
    {

    }
}
