using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SaveGameManager saveGameManager;
    public void NewGameButton()
    {
        PlayerPrefs.SetInt("ShouldLoadGame", 0);
        saveGameManager.DeleteSaveData();
        LevelLoader.Instance.LoadLevel(1, false);
    }

    public void LoadGameButton()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/SaveData"))
        {
            PlayerPrefs.SetInt("ShouldLoadGame", 1);
            LevelLoader.Instance.LoadLevel(1, false);
        }
        else
        {
            Debug.LogWarning("No save file found");

        }
    }



    public void QuitButton()
    {   
        Application.Quit();
    }
}
