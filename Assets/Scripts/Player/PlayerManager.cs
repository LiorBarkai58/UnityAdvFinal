using UnityEngine;


public class PlayerManager : MonoBehaviour {

    private static readonly int Death = Animator.StringToHash("Death");

    [Header("Player Scripts")]
    [SerializeField] private PlayerController playerController;

    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private AbilityManager abilityManager;

    [SerializeField] private PlayerCombatManager playerCombat;

    [SerializeField] private PlayerExperience playerExperience;

    [Header("References")]
    [SerializeField] private Animator animator;



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
        animator.SetTrigger(Death);
        Debug.Log("Player Death");
    }

    private void UpdateStats(){
        abilityManager.SetAttackSpeed(playerStats.GetStatValue(Stats.AttackSpeed));
        playerCombat.UpdateMaxHealth(playerStats.GetStatValue(Stats.MaxHealth));
        playerExperience.setMultiplier(playerStats.GetStatValue(Stats.ExpGain));
    }
}