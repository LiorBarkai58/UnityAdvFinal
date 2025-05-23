using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class DamageZone : MonoBehaviour{

    [SerializeField] private ZoneData zoneData;

    [Header("For particle use - Ok to leave empty")]
    [SerializeField] private ParticleSystem particles;

    [Header("For VFX use - Ok to leave empty")]
    [SerializeField] private VisualEffect VFX;
    private float _damageModifier = 1;

    private List<CombatManager> enemiesInRange = new List<CombatManager>();


    public void SetDamageModifier(float DamageModifier){
        this._damageModifier = DamageModifier;
    }
    private void Start()
    {
        if(particles) particles.Play();
        if(VFX) VFX.Play();
        StartCoroutine(DamageInZone());   
    }

    private IEnumerator DamageInZone(){
        for(int i = 0; i < zoneData.ZoneDuration/zoneData.DamageInterval;i++){
            if(zoneData.IntervalWindup){
                yield return new WaitForSeconds(zoneData.DamageInterval);
            }
            for (int j = enemiesInRange.Count - 1; j >= 0; j--)
            {
                if (enemiesInRange[j] != null)
                    enemiesInRange[j].TakeDamage(new DamageArgs { Damage = zoneData.Damage * _damageModifier });
            }
            yield return new WaitForSeconds(zoneData.DamageInterval);
            
        }
        Destroy(gameObject);
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