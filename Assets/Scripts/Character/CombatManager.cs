using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
    protected float currentHealth = 0;

    public float CurrentHealth => currentHealth;

    protected float currentMaxHealth = 0;

    public float CurrentMaxHealth => currentMaxHealth;

    public event UnityAction<DamageArgs> OnTakeDamage;

    public event UnityAction<CombatManager> OnDeath; 

    public virtual void TakeDamage(DamageArgs damageArgs){
        currentHealth -= damageArgs.Damage;
        OnTakeDamage?.Invoke(damageArgs);
        if(currentHealth <= 0){
            OnDeath?.Invoke(this);
        }
    }

    public void RestoreHealth(float Health){
        currentHealth = Mathf.Clamp(currentHealth + Health, 0, currentMaxHealth);
    }

    
    
}
