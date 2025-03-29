using UnityEngine;
using UnityEngine.Events;



public class EnemyManager : MonoBehaviour {

    private static readonly int Death = Animator.StringToHash("Death");

    [SerializeField] private Animator animator;
    [SerializeField] private EnemyCombatManager combatManager;

    
    public event UnityAction<EnemyManager> OnDeath;

    private void Start()
    {
        combatManager.OnDeath += HandleDeath;
    }

    private void OnEnable(){
        combatManager.Initialize();
    }

    private void HandleDeath(CombatManager combatManager){
        gameObject.SetActive(false);
        OnDeath?.Invoke(this);
        animator.SetTrigger(Death);
    }

    private void OnValidate()
    {
        if(!combatManager){
            combatManager = gameObject.GetComponentInChildren<EnemyCombatManager>();
        }
    }

}