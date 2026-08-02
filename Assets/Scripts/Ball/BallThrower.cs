using UnityEngine;
using UnityEngine.InputSystem;
public class BallThrower : MonoBehaviour
{
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _forwardWeight = 1f;
    [SerializeField] private float _verticalMultiplier = 0.01f;
    [SerializeField] private float _horizontalMultiplier = 0.003f;
    [SerializeField] private float _horizontalDeadZone = 40f;
    [SerializeField] private float _minSpeed = 200f;
    [SerializeField] private float _maxSpeed = 1200f;
    [SerializeField] private float _powerMultiplier = 0.004f;
    private Vector2 _startPos;
    private float _startTime;
    private void Update()
    {
        if (Pointer.current == null) return;

        if (Pointer.current.press.wasPressedThisFrame)
        {
            _startPos = Pointer.current.position.ReadValue();
            _startTime = Time.time;
        }
        if (Pointer.current.press.wasReleasedThisFrame)
        {
            Vector2 endPos = Pointer.current.position.ReadValue();
            float endTime = Time.time;

            Vector2 swipeVector = endPos - _startPos;
            float swipeDuration = endTime - _startTime;

            if (swipeDuration <= 0f) return;
            if (swipeVector.magnitude < 20f) return;

            float speed = swipeVector.magnitude / swipeDuration;
            speed = Mathf.Clamp(speed, _minSpeed, _maxSpeed);

            float yaw = _cameraTransform.eulerAngles.y;
            Quaternion yawRotation = Quaternion.Euler(0f, yaw, 0f);
            Vector3 flatForward = yawRotation * Vector3.forward;
            Vector3 flatRight = yawRotation * Vector3.right;

            float adjustedX = swipeVector.x;
            if (Mathf.Abs(adjustedX) < _horizontalDeadZone)
            {
                adjustedX = 0f;
            }
            float horizontalWeight = adjustedX * _horizontalMultiplier;
            float verticalWeight = swipeVector.y * _verticalMultiplier;

            Vector3 direction = (flatForward * _forwardWeight)
                               + (Vector3.up * verticalWeight)
                               + (flatRight * horizontalWeight);
            direction = direction.normalized;

            float forceMagnitude = speed * _powerMultiplier;
            Vector3 finalForce = direction * forceMagnitude;

            Ball currentBall = _ballSpawner.GetCurrentBall();
            if (currentBall != null)
            {
                currentBall.Throw(finalForce);
                _ballSpawner.SpawnBall();
            }
        }
    }
}