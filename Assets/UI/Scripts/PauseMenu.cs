using UnityEngine;
using UnityEngine.Rendering;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;
    [SerializeField] private GameObject pauseMenuUI;

    private void Start()
    {
        Debug.Log($"pausemenu attatched to {gameObject.name}");
    }
    void Update()
    {
        
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsGamePaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("resuming game");
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsGamePaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
