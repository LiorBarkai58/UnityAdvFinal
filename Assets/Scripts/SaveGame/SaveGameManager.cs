using System.IO;
using UnityEngine;
using System;

public class SaveGameManager : MonoBehaviour
{
    private const string SAVE_FILE_NAME = "/SaveData";

    private SerializedSaveGame serializedSaveGame;

    public static event Action<SerializedSaveGame> OnSave;
    public static event Action<SerializedSaveGame> OnLoad;

    public static SaveGameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnApplicationQuit()
    {
        SaveGame();
    }

    [ContextMenu("Save")]
    
    public void SaveGame()
    {
        Debug.Log("=== SAVING ABILITIES ===");
        serializedSaveGame = new SerializedSaveGame();

        OnSave?.Invoke(serializedSaveGame);

        SaveToJson();

    }

    [ContextMenu("Load")]
    public void LoadGame()
    {
        Debug.Log("=== LOADING ABILITIES ===");

        LoadFromJson();

        OnLoad?.Invoke(serializedSaveGame);

    }

    public void DeleteSaveData()
    {
        string savePath = Application.persistentDataPath + SAVE_FILE_NAME;
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save file deleted");
        }
    }

    private void SaveToJson()
    {
        string jsonString = JsonUtility.ToJson(serializedSaveGame, true);

        File.WriteAllText(Application.persistentDataPath + SAVE_FILE_NAME, jsonString);
    }

    private void LoadFromJson()
    {
        string jsonString = File.ReadAllText(Application.persistentDataPath + SAVE_FILE_NAME);
        serializedSaveGame = JsonUtility.FromJson<SerializedSaveGame>(jsonString);
    }
}
