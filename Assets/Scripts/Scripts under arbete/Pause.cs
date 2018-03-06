using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public static bool isPaused = false;
    public GameObject pausePanel;

    private bool _axisActive;

    private void Update()
    {
        if(Input.GetAxisRaw("P1Start") > 0 || Input.GetAxisRaw("P2Start") > 0)
        {
            if(isPaused && !_axisActive)
            {
                ResumeGame();
                _axisActive = true;
            }
            else if(!isPaused && !_axisActive)
            {
                PauseGame();
                _axisActive = true;
            }
        }

        if(Input.GetAxisRaw("P1Start") == 0 || Input.GetAxisRaw("P2Start") == 0)
        {
            _axisActive = false;
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

}
