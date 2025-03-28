using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode()]
public class UI_ProgressBar : MonoBehaviour
{
    
    [SerializeField] private Image Fill;
    public void SetFillAmount(float current, float max)
    {
        float currentFill = current / max;

        Fill.fillAmount = currentFill;
    }
}
