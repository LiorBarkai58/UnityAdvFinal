using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode()]
public class UI_ProgressBar : MonoBehaviour
{
    public float Maximum;
    public float Current;
    [SerializeField] private Image Fill;
    
    public void SetFillAmount(float current, float max)
    {
        if(Maximum == 0) return;
        Current = current;
        Maximum = max;
        float currentFill = Current / Maximum;

        Fill.fillAmount = currentFill;
    }
}
