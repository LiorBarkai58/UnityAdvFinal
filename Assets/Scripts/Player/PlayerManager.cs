using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class PlayerManager : MonoBehaviour {

    private static readonly int Death = Animator.StringToHash("Death");

    [Header("Player Scripts")]
    [SerializeField] private PlayerController playerController;

    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private AbilityManager abilityManager;

    [SerializeField] private PlayerCombatManager playerCombat;

    [SerializeField] private PlayerExperience playerExperience;

    [SerializeField] private LevelupManager levelupManager;

    [Header("References")]
    [SerializeField] private Animator animator;





    private void Start()
    {
        playerStats.Initialize();
        abilityManager.SetAttackSpeed(playerStats.GetStatValue(Stats.AttackSpeed));

        if (PlayerPrefs.GetInt("ShouldLoadGame", 0) != 1)
        {
            playerCombat.Initialize(playerStats.GetStatValue(Stats.MaxHealth));
        }

        playerCombat.OnDeath += HandleDeath;
        playerStats.OnStatsUpdated += UpdateStats;
        playerExperience.OnLevelUp += HandleLevelUp;
        levelupManager.OnStatUpgrade += HandleStatUpgrade;
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
    }

    private void UpdateStats(){
        abilityManager.SetAttackSpeed(playerStats.GetStatValue(Stats.AttackSpeed));
        playerCombat.UpdateMaxHealth(playerStats.GetStatValue(Stats.MaxHealth));
        playerExperience.setMultiplier(playerStats.GetStatValue(Stats.ExpGain));
    }

    private void HandleLevelUp(){
        StartCoroutine(LevelupSequence());
    }
    private IEnumerator LevelupSequence(){
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.5f);
        levelupManager.gameObject.SetActive(true);
        levelupManager.SetupLevel(abilityManager.Abilities, playerStats.BaseStats.Keys.ToList());
    }
    private void HandleStatUpgrade(Stats stat){
        playerStats.UpgradeStat(stat);
    }


    
}