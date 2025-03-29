using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class SaveGameManager : MonoBehaviour
{
    private const string SAVE_FILE_NAME = "/SaveData";

    private SerializedSaveGame serializedSaveGame;

    [SerializeField] private PlayerTransform playerTransform;
    [SerializeField] private PlayerCombatManager combatManager;
    [SerializeField] private PlayerExperience playerExperience;
    [SerializeField] private AbilityManager abilityManager;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private UI_KillCount killCount;
    [SerializeField] private Timer timer;

    [ContextMenu("Save")]
    
    public void SaveGame()
    {
        Debug.Log("=== SAVING ABILITIES ===");
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

        serializedSaveGame.timer = timer.ElapsedTime;

        Debug.Log($"playerHP: {serializedSaveGame.playerHP}");
        Debug.Log($"XP: {serializedSaveGame.currentEXP}");
        Debug.Log($"Level: {serializedSaveGame.level}");


        foreach (Ability ability in abilityManager.Abilities)
        {
            Debug.Log($"Saving ability ID: {ability.AbilityID}, Type: {ability.GetType().Name}, Level: {ability.Level}");
            SerializedAbility serializedAbility = new SerializedAbility();
            serializedAbility.abilityID = ability.AbilityID;
            serializedAbility.abilityLevel = ability.Level;
            serializedSaveGame.abilities.Add(serializedAbility);
        }

        foreach (Stats statType in System.Enum.GetValues(typeof(Stats)))
        {
            Debug.Log($"Saving Stat: {statType} Modifier: {playerStats.GetStatModifier(statType)}");
            SerializedStat serializedStat = new SerializedStat();
            serializedStat.statType = statType;
            serializedStat.statModifier = playerStats.GetStatModifier(statType);
            serializedSaveGame.stats.Add(serializedStat);
        }

            SaveToJson();

    }
    [ContextMenu("Load")]
    public void LoadGame()
    {
        Debug.Log("=== LOADING ABILITIES ===");

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

        Debug.Log($"playerHP: {combatManager.CurrentHealth}");
        Debug.Log($"XP: {playerExperience.CurrentEXP}");
        Debug.Log($"Level: {playerExperience.Level}");

        Dictionary<int, Ability> abilityMap = new Dictionary<int, Ability>();

        foreach (Ability ability in abilityManager.Abilities)
        {
            abilityMap[ability.AbilityID] = ability;
        }

        foreach (SerializedAbility serializedAbility in serializedSaveGame.abilities)
        {
            Debug.Log($"Loading ability ID: {serializedAbility.abilityID}, Level: {serializedAbility.abilityLevel}");
            if (abilityMap.TryGetValue(serializedAbility.abilityID, out Ability ability))
            {
                ability.ResetLevel();

                for (int i = 1; i < serializedAbility.abilityLevel; i++)
                {
                    ability.UpgradeAbility();
                }
            }
        }

        playerStats.ResetModifiers();

        foreach (SerializedStat serializedStat in serializedSaveGame.stats)
        {
            Debug.Log($"Loading Stat: {serializedStat} modifier: {serializedStat.statModifier}");
            playerStats.SetModifier(serializedStat.statType, serializedStat.statModifier);
        }

        combatManager.LoadHealth();
        playerExperience.LoadXP(serializedSaveGame.currentEXP);
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
