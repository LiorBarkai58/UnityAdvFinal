using UnityEngine;


public class Projectile : MonoBehaviour{
    private Vector3 Direction;

    [SerializeField] private ProjectileData projectileData;

    [SerializeField] private bool PlayerProjectile;


    public void SetDirection(Vector3 direction){
        this.Direction = direction;
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
                enemyCombat.TakeDamage(new DamageArgs{ Damage = projectileData.Damage});
                Destroy(gameObject);
            }
        }
    }
}