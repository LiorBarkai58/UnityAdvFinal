using UnityEngine;

[CreateAssetMenu(menuName = "Combat/CombatData")]
public class CombatData : ScriptableObject {
    [SerializeField] private float maxHealth;

    [SerializeField] private float baseDamage;

    [SerializeField] private bool takeKnockback = false;


    public float MaxHealth => maxHealth;

    public float BaseDamage => baseDamage;

    public bool TakeKnockback => takeKnockback;
}