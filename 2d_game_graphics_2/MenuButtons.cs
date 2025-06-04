using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MenuButtons : MonoBehaviour
{
    private bool isPaused = false;

    public void OnStartGameButtonClicked()
    {
        Debug.Log("Game Start.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void NextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
             Debug.Log("No more levels! You are on the last scene.");
            // Optionally: load a main menu or restart first level
            // SceneManager.LoadScene(0);
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        if (Time.timeScale == 1)
        {
            Debug.Log("Unpaused.");
        }
        else
        {
            Debug.Log("Paused.");
        }
    }
}
