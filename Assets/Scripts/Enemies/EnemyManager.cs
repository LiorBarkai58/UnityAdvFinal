using UnityEngine;
using UnityEngine.Events;



public class EnemyManager : MonoBehaviour {

    private static readonly int Death = Animator.StringToHash("Death");

    [SerializeField] private Animator animator;
    [SerializeField] private EnemyCombatManager combatManager;
    [SerializeField] private EnemyMovement movement;
    
    public event UnityAction<EnemyManager> OnDeath;

    private void Start()
    {
        combatManager.OnDeath += HandleDeath;
    }

    private void HandleDeath(CombatManager combatManager){
        OnDeath?.Invoke(this);
        movement.canMove = false;
        animator.SetTrigger(Death);
        Destroy(gameObject, 5);
       // gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        if(!combatManager){
            combatManager = gameObject.GetComponentInChildren<EnemyCombatManager>();
        }
    }

}