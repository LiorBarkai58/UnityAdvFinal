using UnityEngine;
using UnityEngine.Events;



public class EnemyManager : MonoBehaviour {
    [SerializeField] private CombatManager combatManager;
    
    public event UnityAction OnEnemyKilled;

    private void Start()
    {
        combatManager.OnDeath += HandleDeath;
    }

    private void HandleDeath(CombatManager combatManager){
        gameObject.SetActive(false);
        OnEnemyKilled?.Invoke();
    }

    private void OnValidate()
    {
        if(!combatManager){
            combatManager = gameObject.GetComponentInChildren<CombatManager>();
        }
    }

}