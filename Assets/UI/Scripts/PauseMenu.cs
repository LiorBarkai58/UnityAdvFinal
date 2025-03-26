using UnityEngine;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public GameObject pauseMenuUI;

    private void Start()
    {
        Debug.Log($"pausemenu attatched to {gameObject.name}");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        Debug.Log("resuming game");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;
        Debug.Log($"timescale is now {Time.timeScale}");
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;
    }
}
