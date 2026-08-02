using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateText;
        UpdateText(ScoreManager.Instance.GetCurrentScore());
    }

    private void OnDisable()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= UpdateText;
        }
    }

    private void UpdateText(int newScore)
    {
        _scoreText.text = "Score: " + newScore;
    }
}