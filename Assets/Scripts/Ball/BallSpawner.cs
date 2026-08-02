using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Transform _spawnPoint;
    private ObjectPool<Ball> _ballPool;
    private Ball _currentBall;

    private void Awake()
    {
        _ballPool = new ObjectPool<Ball>(_ballPrefab);
    }

    private void Start()
    {
        SpawnBall();
    }

    public void SpawnBall()
    {
        Ball item = _ballPool.Get();
        item.BallFollowTarget(_spawnPoint);
        _currentBall = item;

        item.OnBallSettled += HandleBallSettled;
    }

    private void HandleBallSettled(Ball ball)
    {
        ball.OnBallSettled -= HandleBallSettled;
        _ballPool.Return(ball);
    }

    public Ball GetCurrentBall()
    {
        return _currentBall;
    }
}