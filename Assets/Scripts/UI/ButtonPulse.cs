using UnityEngine;

public class ButtonPulse : MonoBehaviour
{
    [SerializeField] private float _scaleAmount = 0.05f;
    [SerializeField] private float _speed = 2f;
    private Vector3 _baseScale;

    void Start()
    {
        _baseScale = transform.localScale;
    }

    void Update()
    {
        float pulse = 1f + Mathf.Sin(Time.time * _speed) * _scaleAmount;
        transform.localScale = _baseScale * pulse;
    }
}