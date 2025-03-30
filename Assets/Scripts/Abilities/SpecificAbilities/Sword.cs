using DG.Tweening;
using UnityEngine;


public class Sword : RuneCircle
{
    [SerializeField] private ParticleSystem effect;

    private void Start()
    {
        _level = 0;
    }
    public override bool AbilityLogic()
    {
        effect.Play();
        base.AbilityLogic();
        return true;
    }

    public override void UpgradeAbility()
    {
        base.UpgradeAbility();
        if(_level%3 == 0){
            _personalAttackSpeedMultiplier += 0.15f;
        }
        else if(_level %2 == 0){
            Vector3 endValue = this.transform.localScale * 1.15f;
            endValue.y = 1;
            transform.DOScale(endValue, 0.3f);//Increase scale by 15% per second level

            var mainModule = effect.main; // Get a copy of the MainModule struct
            mainModule.startSizeMultiplier *= 1.15f;
            
        }
        else{
            _damageModifier+= 0.1f;//Increase damage by 10% every second level
        }
    }
}