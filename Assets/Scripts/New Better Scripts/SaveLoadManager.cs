using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour {

    public static int cpIndex;
    public static int sceneIndex;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            Save();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            Load();
        }
    }

    public void Save()
    {
        for(int i = 0; i < CheckPointManager.checkPoints.Count; i++)
        {
            if(CheckPointManager.checkPoints[i] == CheckPointManager.activeCheckpoint)
            {
                cpIndex = i;
            }
        }

        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        PlayerPrefs.SetInt("CP", cpIndex);

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("Scene", sceneIndex);

        //Time.timeScale = 1f;
    }

    public void Load()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(PlayerPrefs.GetInt("Scene", 0));
    }

}
