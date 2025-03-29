using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AbilityCard : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI description;
    private Ability ability;

    public event UnityAction<Ability> OnSelected;
    public void SelectUpgrade(){
        OnSelected?.Invoke(ability);
    }

    public void SetAbility(Ability ability){
        if(ability){
            this.ability = ability;
            if(description) description.SetText(ability.name);
        }
    }

    
}