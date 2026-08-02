using System;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }

    public event Action<float> OnTimeChanged;
    public event Action OnTimerEnded;

    [SerializeField] private float _startingTime = 60f;
    private float _timeRemaining;
    private bool _isRunning;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _isRunning = false; 
    }
    private void Update()
    {
        if (!_isRunning) return;

        _timeRemaining -= Time.deltaTime;
        OnTimeChanged?.Invoke(_timeRemaining);

        if (_timeRemaining <= 0f)
        {
            _timeRemaining = 0f;
            _isRunning = false;
            OnTimerEnded?.Invoke();
        }
    }

    public void RestartTimer()
    {
        _timeRemaining = _startingTime;
        _isRunning = false; 
    }

    public void BeginTimer()
    {
        if (GameSession.CurrentMode == GameMode.Practice)
        {
            _isRunning = false; // pauses timer for practice

            return;
        }

        _timeRemaining = _startingTime;
        _isRunning = true;
    }
}