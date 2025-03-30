using UnityEngine;

[CreateAssetMenu(menuName = "Projectiles/ProjectileData")]
public class ProjectileData : ScriptableObject {
    [SerializeField] private float damage = 1;

    [SerializeField] private float projectileSpeed = 5;


    public float Damage => damage;

    public float ProjectileSpeed => projectileSpeed;

}