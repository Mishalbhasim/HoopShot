using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public event Action<int> OnScoreChanged;

    private int _currentScore;
    private int _bestScore;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    public void AddScore(int points)
    {
        _currentScore += points;
        OnScoreChanged?.Invoke(_currentScore);

        if (GameSession.CurrentMode == GameMode.Timed && _currentScore > _bestScore)
        {
            _bestScore = _currentScore;
            PlayerPrefs.SetInt("BestScore", _bestScore);
        }
    }

    public void ResetScore()
    {
        _currentScore = 0;
        OnScoreChanged?.Invoke(_currentScore);
    }

    public int GetCurrentScore() => _currentScore;
    public int GetBestScore() => _bestScore;
}