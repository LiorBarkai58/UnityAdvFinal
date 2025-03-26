using TMPro;
using UnityEngine;

public class UI_KillCount : MonoBehaviour
{
    private int killsNumber;
    private EnemyManager enemyManager;
    public TextMeshProUGUI counterText;

    void Start()
    {
        killsNumber = 0;
        enemyManager.OnEnemyKilled += RegisterKill;
        counterText.SetText(killsNumber.ToString());
    }

    private void RegisterKill()
    {
        Debug.Log("enemy killed");
        killsNumber++;
        counterText.SetText(killsNumber.ToString());
    }
}
