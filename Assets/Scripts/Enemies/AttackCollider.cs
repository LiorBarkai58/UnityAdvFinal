using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    [SerializeField] private CombatManager CombatManager;


    private bool isPlayerInCollider = false;

    public void ApplyDamage()
    {
        if(isPlayerInCollider && CombatManager) 
        {
            //CombatManager.TakeDamage();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInCollider = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerInCollider = false;
        }
    }
}
