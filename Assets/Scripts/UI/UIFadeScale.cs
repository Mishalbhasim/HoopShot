using System.Collections;
using UnityEngine;

public class UIFadeScale : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _duration = 0.3f;

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(AnimateIn());
    }

    IEnumerator AnimateIn()
    {
        float t = 0f;
        transform.localScale = Vector3.one * 0.9f;
        _canvasGroup.alpha = 0f;
        while (t < _duration)
        {
            t += Time.unscaledDeltaTime; 
            float p = t / _duration;
            _canvasGroup.alpha = p;
            transform.localScale = Vector3.Lerp(Vector3.one * 0.9f, Vector3.one, p);
            yield return null;
        }
        _canvasGroup.alpha = 1f;
        transform.localScale = Vector3.one;
    }
}