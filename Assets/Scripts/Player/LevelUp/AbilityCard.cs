using UnityEngine;
using UnityEngine.Events;

public class AbilityCard : MonoBehaviour {
    private Ability ability;

    public event UnityAction<Ability> OnSelected;
    public void SelectUpgrade(){
        OnSelected?.Invoke(ability);
    }

    public void SetAbility(Ability ability){
        if(ability){
            this.ability = ability;
        }
    }

    
}