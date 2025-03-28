using UnityEngine;


public class Projectile : MonoBehaviour{
    private Vector3 Direction;

    private float DamageModifier = 1;

    [SerializeField] private ProjectileData projectileData;

    [SerializeField] private bool PlayerProjectile;


    public void SetDirection(Vector3 direction){
        this.Direction = direction;
    }
    public void SetDamageModifier(float DamageModifier){
        this.DamageModifier = DamageModifier;
    }

    private void Update()
    {
        transform.position += Direction *projectileData.ProjectileSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Enemy")){
            CombatManager enemyCombat = collision.GetComponent<CombatManager>();
            if(enemyCombat){
                enemyCombat.TakeDamage(new DamageArgs{ Damage = projectileData.Damage * DamageModifier});
                Destroy(gameObject);
            }
        }
    }
}