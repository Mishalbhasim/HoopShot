using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _finalScoreText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private GameObject _mainMenuPanel;

    private void OnEnable()
    {
        GameTimer.Instance.OnTimerEnded += ShowGameOver;
    }

    private void OnDisable()
    {
        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.OnTimerEnded -= ShowGameOver;
        }
    }

    private void ShowGameOver()
    {
        _panel.SetActive(true);
        _finalScoreText.text = "Score: " + ScoreManager.Instance.GetCurrentScore();

        bool isTimed = GameSession.CurrentMode == GameMode.Timed;
        _bestScoreText.gameObject.SetActive(isTimed);
        if (isTimed)
            _bestScoreText.text = "Best: " + ScoreManager.Instance.GetBestScore();

        Time.timeScale = 0f;
    }
    public void OnEndPressed()
    {
        GameTimer.Instance.RestartTimer();
        ShowGameOver();
    }

    public void OnMainMenuPressed()
    {
        Time.timeScale = 1f;
        _panel.SetActive(false);
        ScoreManager.Instance.ResetScore();
        GameTimer.Instance.RestartTimer();
        FindObjectOfType<HoopPlacer>().ResetHoop();
        _mainMenuPanel.SetActive(true);
    }
    public void OnRestartPressed()
    {
        Time.timeScale = 1f;
        _panel.SetActive(false);

        ScoreManager.Instance.ResetScore();
        GameTimer.Instance.RestartTimer();
        FindObjectOfType<HoopPlacer>().ResetHoop();
    }
}