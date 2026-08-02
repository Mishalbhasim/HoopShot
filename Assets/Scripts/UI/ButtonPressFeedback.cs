using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPressFeedback : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float _pressScale = 0.92f;
    private Vector3 _originalScale;

    void Awake()
    {
        _originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = _originalScale * _pressScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = _originalScale;
    }
}