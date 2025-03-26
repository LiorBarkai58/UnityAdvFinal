using UnityEngine;


public class PlayerManager : MonoBehaviour {

    private static readonly int Death = Animator.StringToHash("Death");

    [SerializeField] private PlayerController playerController;

    [SerializeField] private CombatManager playerCombat;

    [SerializeField] private Animator animator;



    private void Start()
    {
        playerCombat.OnDeath += HandleDeath;
    }

    private void OnValidate()
    {
        if(!playerCombat){
            playerCombat = gameObject.GetComponentInChildren<CombatManager>();
        }
        if(!playerController){
            playerController = gameObject.GetComponent<PlayerController>();
        }
    }

    private void HandleDeath(CombatManager combatManager){
        //gameObject.SetActive(false);
        animator.SetTrigger(Death);
        Debug.Log("Player Death");
    }
}