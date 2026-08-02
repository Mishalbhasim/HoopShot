using UnityEngine;
using TMPro;

public class TimerDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    private void OnEnable()
    {
        if (GameSession.CurrentMode == GameMode.Practice)
        {
            gameObject.SetActive(false);
            return;
        }
        GameTimer.Instance.OnTimeChanged += UpdateText;
    }

    private void OnDisable()
    {
        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.OnTimeChanged -= UpdateText;
        }
    }

    private void UpdateText(float time)
    {
        _timerText.text = "Time: " + Mathf.CeilToInt(time);
    }
}