using UnityEngine;
using UnityEngine.Events;



public class EnemyManager : MonoBehaviour {

    private static readonly int Death = Animator.StringToHash("Death");

    [SerializeField] private CombatManager combatManager;
    [SerializeField] private Animator animator;
    
    public event UnityAction OnEnemyKilled;

    private void Start()
    {
        combatManager.OnDeath += HandleDeath;
    }

    private void HandleDeath(CombatManager combatManager){
        //gameObject.SetActive(false);
        animator.SetTrigger(Death);
        OnEnemyKilled?.Invoke();
    }

    private void OnValidate()
    {
        if(!combatManager){
            combatManager = gameObject.GetComponentInChildren<CombatManager>();
        }
    }

}