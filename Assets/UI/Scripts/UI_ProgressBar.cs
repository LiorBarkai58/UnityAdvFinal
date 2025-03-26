using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode()]
public class UI_ProgressBar : MonoBehaviour
{
    public int maximum;
    public int current;
    public Image Fill;

    private void Update()
    {
        SetFillAmount();
    }
    private void SetFillAmount()
    {
        float currentFill = (float)current / (float)maximum;

        Fill.fillAmount = currentFill;
    }
}
