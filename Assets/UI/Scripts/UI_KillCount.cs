using UnityEngine;

public class UI_KillCount : MonoBehaviour
{
    private int kills;
    private CombatManager combatManager;
    private EnemyManager enemyManager;

    void Start()
    {
        kills = 0;
        combatManager.OnDeath += RegisterKill;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RegisterKill(CombatManager combatManager)
    {
        kills++;
    }
}
