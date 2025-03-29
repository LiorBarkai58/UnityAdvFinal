using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class StatCard : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI description;

    private Stats stat;

    public event UnityAction<Stats> OnSelected;
    public void SelectUpgrade(){
        OnSelected?.Invoke(stat);
    }

    public void SetStat(Stats stat){
        this.stat = stat;
        if(description) description.SetText(stat.ToString());
    }

    
}