using UnityEngine;
using UnityEngine.Events;



public class EnemyManager : MonoBehaviour {
    [SerializeField] private EnemyCombatManager combatManager;
    
    public event UnityAction OnDeath;

    private void Start()
    {
        combatManager.OnDeath += HandleDeath;
    }

    private void HandleDeath(CombatManager combatManager){
        gameObject.SetActive(false);
        OnDeath?.Invoke();
    }

    private void OnValidate()
    {
        if(!combatManager){
            combatManager = gameObject.GetComponentInChildren<EnemyCombatManager>();
        }
    }

}