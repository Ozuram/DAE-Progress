using UnityEngine;

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
