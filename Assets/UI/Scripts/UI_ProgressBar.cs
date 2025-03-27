using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode()]
public class UI_ProgressBar : MonoBehaviour
{
    public int Maximum;
    public int Current;
    [SerializeField] private Image Fill;
    
    public void SetFillAmount(float current, float max)
    {
        current = Current;
        max = Maximum;
        float currentFill = (float)Current / (float)Maximum;

        Fill.fillAmount = currentFill;
    }
}
