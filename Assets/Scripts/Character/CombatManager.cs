using UnityEngine;
using UnityEngine.Events;

public class CombatManager : MonoBehaviour
{
    [SerializeField] protected UI_ProgressBar healthBar;

    protected float currentHealth = 0;

    public float CurrentHealth => currentHealth;

    protected float currentMaxHealth = 0;

    public float CurrentMaxHealth => currentMaxHealth;

    public event UnityAction<DamageArgs> OnTakeDamage;

    public event UnityAction<CombatManager> OnDeath;

    private void Start()
    {
        healthBar.SetFillAmount(currentHealth, currentMaxHealth);
    }
    public virtual void TakeDamage(DamageArgs damageArgs){
        currentHealth -= damageArgs.Damage;
        OnTakeDamage?.Invoke(damageArgs);
        if(currentHealth <= 0){
            OnDeath?.Invoke(this);
        }
        healthBar.SetFillAmount(currentHealth, currentMaxHealth);
    }

    public void RestoreHealth(float Health){
        currentHealth = Mathf.Clamp(currentHealth + Health, 0, currentMaxHealth);
        healthBar.SetFillAmount(currentHealth, currentMaxHealth);
    }

    
    
}
