using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        playerController.OnPlayerPause += PauseUnPause;
    }
    public void PauseUnPause()
    {
        if (IsGamePaused)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            IsGamePaused = false;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("resuming game");
        }
        else
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            IsGamePaused = true;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void OptionsButton()
    {
        Debug.Log("options");
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(1);
    }
}
