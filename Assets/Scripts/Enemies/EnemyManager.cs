using UnityEngine;
using UnityEngine.Events;
using System.Collections;


public class EnemyManager : MonoBehaviour {

    [SerializeField] private Animator animator;
    [SerializeField] private EnemyCombatManager combatManager;
    [SerializeField] private EnemyMovement movement;

    
    public event UnityAction<EnemyManager> OnDeath;

    
    private void Start()
    {
        combatManager.OnDeath += HandleDeath;
    }

    public void UpdateFunction(){
        movement.UpdateFunction();
    }

    private void OnEnable(){
        combatManager.Initialize();
        movement.canMove = true;
    }

    private void HandleDeath(CombatManager combatManager){
        OnDeath?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        if(!combatManager){
            combatManager = gameObject.GetComponentInChildren<EnemyCombatManager>();
        }
    }

    public void SetMaxhpMultiplier(float maxHPmultiplier){
        combatManager.Initialize(maxHPmultiplier);
    }   


}