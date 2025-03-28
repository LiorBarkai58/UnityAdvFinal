using UnityEngine;


public class Sword : Ability
{
    [SerializeField] private ParticleSystem effect;
    public override bool AbilityLogic()
    {
        effect.Play();
        return true;
    }
}