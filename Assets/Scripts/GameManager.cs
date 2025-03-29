using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SaveGameManager saveGameManager;

    private void Start()
    {
        if (PlayerPrefs.GetInt("ShouldLoadGame", 0) == 1)
        {
            LoadGame();
            PlayerPrefs.SetInt("ShouldLoadGame", 0);
        }
    }

    private void LoadGame()
    {
        if (saveGameManager != null)
        {
            saveGameManager.LoadGame();
            Debug.Log("Game loaded!");
        }
        else
        {
            Debug.LogError("SaveGameManager reference is missing!");
        }
    }

    public void SaveGame()
    {
        if (saveGameManager != null)
        {
            saveGameManager.SaveGame();
            Debug.Log("Game saved successfully");
        }
        else
        {
            Debug.LogError("SaveGameManager reference is missing!");
        }
    }
}