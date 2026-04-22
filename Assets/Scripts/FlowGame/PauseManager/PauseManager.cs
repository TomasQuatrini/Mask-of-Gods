using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool IsPaused { get; private set; }
    public bool IsRunning { get; private set; }

    [SerializeField] private GameObject pauseMenu;

    public void PauseGame()
    {
        IsPaused = true;
        IsRunning = false;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        IsPaused = false;
        IsRunning = true;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void TogglePause()
    {
        if (IsPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
}

