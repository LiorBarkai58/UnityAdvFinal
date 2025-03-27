using UnityEngine;
using UnityEngine.Events;



public class EnemyManager : MonoBehaviour {
    [SerializeField] private EnemyCombatManager combatManager;
    
    public event UnityAction<EnemyManager> OnDeath;

    private void Start()
    {
        combatManager.OnDeath += HandleDeath;
    }

    private void HandleDeath(CombatManager combatManager){
        gameObject.SetActive(false);
        OnDeath?.Invoke(this);
    }

    private void OnValidate()
    {
        if(!combatManager){
            combatManager = gameObject.GetComponentInChildren<EnemyCombatManager>();
        }
    }

}