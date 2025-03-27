using UnityEngine;


public class PlayerManager : MonoBehaviour {
    [SerializeField] private PlayerController playerController;

    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private AbilityManager abilityManager;

    [SerializeField] private PlayerCombatManager playerCombat;



    private void Start()
    {
        playerStats.Initialize();
        abilityManager.SetAttackSpeed(playerStats.GetStatValue(Stats.AttackSpeed));
        playerCombat.Initialize(playerStats.GetStatValue(Stats.MaxHealth));
        playerCombat.OnDeath += HandleDeath;
    }

    private void OnValidate()
    {
        if(!playerCombat){
            playerCombat = gameObject.GetComponentInChildren<PlayerCombatManager>();
        }
        if(!playerController){
            playerController = gameObject.GetComponent<PlayerController>();
        }
    }

    private void HandleDeath(CombatManager combatManager){
        gameObject.SetActive(false);
        Debug.Log("Player Death");
    }
}