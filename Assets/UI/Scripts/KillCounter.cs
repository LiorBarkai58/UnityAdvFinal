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

    private void UpdateCounter(int currentKills){
        killCounter.SetText(currentKills.ToString());
    }
}