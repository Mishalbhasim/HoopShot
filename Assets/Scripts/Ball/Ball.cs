using System;
using UnityEngine;

public class Ball : MonoBehaviour, IPoolable
{
    [SerializeField] private float _settleVelocityThreshold = 0.05f;
    [SerializeField] private float _settleTimeRequired = 1f;
    [SerializeField] private float _outOfBoundsY = -10f;

    private Rigidbody _rigidbody;
    private Transform _followTarget;
    private bool _isHeld;
    private float _settleTimer;

    public event Action<Ball> OnBallSettled;

    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isHeld)
        {
            transform.position = _followTarget.position;
            return;
        }

        if (transform.position.y < _outOfBoundsY)
        {
            OnBallSettled?.Invoke(this);
            return;
        }

        if (_rigidbody.velocity.magnitude < _settleVelocityThreshold)
        {
            _settleTimer += Time.deltaTime;
            if (_settleTimer >= _settleTimeRequired)
            {
                OnBallSettled?.Invoke(this);
            }
        }
        else
        {
            _settleTimer = 0f;
        }
    }

    public void BallFollowTarget(Transform target)
    {
        _followTarget = target;
        _isHeld = true;
        _rigidbody.isKinematic = true;
    }

    public void Throw(Vector3 force)
    {
        _isHeld = false;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(force, ForceMode.Impulse);
    }

    public void OnSpawn()
    {
        _rigidbody.velocity = Vector3.zero;
        _settleTimer = 0f;
    }

    public void OnDespawn()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}