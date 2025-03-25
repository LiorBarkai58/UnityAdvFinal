using UnityEngine;


public class PlayerManager : MonoBehaviour {
    [SerializeField] private PlayerController playerController;

    [SerializeField] private CombatManager playerCombat;



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

    private void HandleDeath(){
        gameObject.SetActive(false);
        Debug.Log("Player Death");
    }
}