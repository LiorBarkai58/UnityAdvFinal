using TMPro;
using UnityEngine;


public class KillCounter : MonoBehaviour {
    [SerializeField] private EnemiesManager enemiesManager;
    [SerializeField] private TextMeshProUGUI killCounter;

    private void Start()
    {
        if(enemiesManager) enemiesManager.OnKillUpdated += UpdateCounter;
        else Debug.LogWarning("Missing enemiesmanager reference in killcounter");
    }

    private void OnEnable()
    {
        SaveGameManager.OnSave += SaveKillCount;
        SaveGameManager.OnLoad += LoadKillCount;
    }

    private void OnDisable()
    {
        SaveGameManager.OnSave -= SaveKillCount;
        SaveGameManager.OnLoad -= LoadKillCount;
    }

    private void UpdateCounter(int currentKills){
        killCounter.SetText(currentKills.ToString());
    }

    private void SaveKillCount(SerializedSaveGame saveData)
    {
        saveData.killCount = enemiesManager.KillCount;
    }

    private void LoadKillCount(SerializedSaveGame saveData)
    {
        enemiesManager.KillCount = saveData.killCount;
        UpdateCounter(enemiesManager.KillCount);
    }

}