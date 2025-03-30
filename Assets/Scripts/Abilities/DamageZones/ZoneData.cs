using UnityEngine;


[CreateAssetMenu(menuName = "Abilities/ZoneData")]
public class ZoneData : ScriptableObject {
    [SerializeField] private float damage;

    public float Damage => damage;

    [SerializeField] private float damageInterval;

    public float DamageInterval => damageInterval;

    [SerializeField] private float zoneDuration;

    public float ZoneDuration => zoneDuration;

    [SerializeField] private bool intervalWindup;//If the particle need an extra interval before doing damage

    public bool IntervalWindup => intervalWindup;
}