using UnityEngine;
using System.Collections.Generic;

public abstract class Ability : MonoBehaviour {

    [SerializeField] private float Cooldown;
    [SerializeField] private List<AudioClip> soundEffectPool;
    private float elapsedCooldown = 0;

    private bool onCooldown = false;

    public int AbilityID => GetType().Name.GetHashCode();

    protected int _level = 1;

    public int Level => _level;

    protected float _damageModifier = 1;

    protected float _personalAttackSpeedMultiplier = 1;

    protected List<CombatManager> enemiesInRange = new List<CombatManager>();



    //Attackspeed multiplier is a way to reduce the cooldown and increase the recharge rate of abilities
    public void AbilityUpdate(float AttackSpeedMultiplier = 1)//Ran by the ability manager and no personal update function
    {
        if(onCooldown){
            elapsedCooldown += Time.deltaTime * AttackSpeedMultiplier * _personalAttackSpeedMultiplier;
            if(elapsedCooldown > Cooldown){
                onCooldown = false;
                elapsedCooldown = 0;
            }
        }   
    }
    public virtual void Activate(){
        if(!onCooldown){
            if(AbilityLogic()){
                onCooldown = true;
                if (soundEffectPool.Count > 0)
                {
                    AudioClip soundEffect = soundEffectPool[Random.Range(0, soundEffectPool.Count)];
                    if(AudioManager.Instance) AudioManager.Instance.PlaySFX(soundEffect);
                }
            }
        }
    }

    public abstract bool AbilityLogic();//returns true if the ability was used successfully

    public virtual void UpgradeAbility(){
        _level++;
    }

    public virtual void ResetLevel()
    {
        _level = 1;
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy")){
            CombatManager enemyCombat = other.GetComponent<CombatManager>();
            if(enemyCombat){
                enemiesInRange.Add(enemyCombat);
                enemyCombat.OnDeath += HandleEnemyDeath;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy")){
            CombatManager enemyCombat = other.GetComponent<CombatManager>();
            if(enemyCombat && enemiesInRange.Contains(enemyCombat)){
                enemiesInRange.Remove(enemyCombat);
                enemyCombat.OnDeath -= HandleEnemyDeath;

            }
        }
    }

    private void HandleEnemyDeath(CombatManager enemy){
        if(enemiesInRange.Contains(enemy)){
            enemiesInRange.Remove(enemy);
        }
    }

    
}