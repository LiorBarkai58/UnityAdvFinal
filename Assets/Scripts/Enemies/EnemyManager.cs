using UnityEngine;



public class EnemyManager : MonoBehaviour {
    [SerializeField] private CombatManager combatManager;

    private void Start()
    {
        combatManager.OnDeath += HandleDeath;
    }

    private void HandleDeath(CombatManager combatManager){
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        if(!combatManager){
            combatManager = gameObject.GetComponentInChildren<CombatManager>();
        }
    }

}