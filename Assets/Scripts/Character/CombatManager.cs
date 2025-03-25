using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
    [SerializeField] private CombatData combatData;

    private float currentHealth = 0;

    public event UnityAction<DamageArgs> OnTakeDamage;

    public event UnityAction OnDeath; 


    private void OnEnable()
    {
        currentHealth = combatData.MaxHealth;   
    }

    public void TakeDamage(DamageArgs damageArgs){
        currentHealth -= damageArgs.Damage;
        if(currentHealth <= 0){
            OnDeath?.Invoke();
        }
    }

    
}
