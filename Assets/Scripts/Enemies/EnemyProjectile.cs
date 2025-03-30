using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private Vector3 Direction;

    [SerializeField] private ProjectileData projectileData;

    public void SetDirection(Vector3 direction)
    {
        this.Direction = direction;
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        transform.position += Direction * projectileData.ProjectileSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.tag);
        Debug.Log(collision.gameObject);
        if (collision.CompareTag("Player"))
        {
            CombatManager playerCombat = collision.GetComponent<CombatManager>();
            if (playerCombat)
            {
                Debug.Log($"Applying damage: {projectileData.Damage}");
                playerCombat.TakeDamage(new DamageArgs { Damage = projectileData.Damage });
                Destroy(gameObject);
            }
        }
    }
}
