using System.IO;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    private const string SAVE_FILE_NAME = "/SaveData";

    private SerializedSaveGame serializedSaveGame;

    [SerializeField] private PlayerTransform playerTransform;
    [SerializeField] private PlayerCombatManager combatManager;
    [SerializeField] private PlayerExperience playerExperience;

    [ContextMenu("Save")]
    
    public void SaveGame()
    {
        serializedSaveGame = new SerializedSaveGame();

        serializedSaveGame.playerPositionX = playerTransform.PlayersTransform.position.x;
        serializedSaveGame.playerPositionY = playerTransform.PlayersTransform.position.y;
        serializedSaveGame.playerPositionZ = playerTransform.PlayersTransform.position.y;

        serializedSaveGame.playerRotationX = playerTransform.PlayersTransform.rotation.x;
        serializedSaveGame.playerRotationY = playerTransform.PlayersTransform.rotation.y;
        serializedSaveGame.playerRotationZ = playerTransform.PlayersTransform.rotation.z;

        serializedSaveGame.playerHP = combatManager.CurrentHealth;

        serializedSaveGame.currentEXP = playerExperience.CurrentEXP;
        serializedSaveGame.level = playerExperience.Level;

        SaveToJson();

    }

    public void LoadGame()
    {
        LoadFromJson();

        playerTransform.PlayersTransform.position = new Vector3(
            serializedSaveGame.playerPositionX, 
            serializedSaveGame.playerPositionY, 
            serializedSaveGame.playerPositionZ);

        playerTransform.PlayersTransform.eulerAngles = new Vector3(
            serializedSaveGame.playerRotationX,
            serializedSaveGame.playerRotationY,
            serializedSaveGame.playerRotationZ);

        combatManager.CurrentHealth = serializedSaveGame.playerHP;
        playerExperience.CurrentEXP = serializedSaveGame.currentEXP;
        playerExperience.Level = serializedSaveGame.level;
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
