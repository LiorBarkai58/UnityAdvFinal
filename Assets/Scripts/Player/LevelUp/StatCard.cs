using UnityEngine;
using UnityEngine.Events;

public class StatCard : MonoBehaviour {
    private Stats stat;

    public event UnityAction<Stats> OnSelected;
    public void SelectUpgrade(){
        OnSelected?.Invoke(stat);
    }

    public void SetStat(Stats stat){
        this.stat = stat;
    }

    
}