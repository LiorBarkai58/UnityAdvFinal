using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public   TextMeshProUGUI _timerText;
    private float _elapsedTime;


    void Update()
    {
        _elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(_elapsedTime / 60);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60);
        _timerText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }
}
