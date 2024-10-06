using System;
using System.Collections;
using UnityEngine;

public class HealthPanel : MonoBehaviour
{
    private bool _isVisible;
    private RectTransform _rectTransform;

    [SerializeField] private Animation[] heartAnimations; 
    private void Awake()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
    }


    public void DeductHealth(int healthLeft)
    {
        if (healthLeft < 0) return;
        heartAnimations[healthLeft].Play();
        // CancelInvoke();
        // if (!_isVisible)
        // {
        //     _isVisible = true;
        //     StartCoroutine(Slide(_rectTransform.anchoredPosition, new Vector2(96, 0), 1f, () =>
        //     {
        //         heartAnimations[healthLeft].Play();
        //         Invoke(nameof(Close), 2f);
        //     }));
        // }
        // else
        // {
        //     heartAnimations[healthLeft].Play();
        //     Invoke(nameof(Close), 2f);
        // }
    }

    private void Close()
    {
        _isVisible = true;
        StartCoroutine(Slide(_rectTransform.anchoredPosition, new Vector2(96, 200), 1f));
    }

    private IEnumerator Slide(Vector2 start, Vector2 end, float duration, Action callback = null)
    {
        float timer = 0;

        while (timer < duration)
        {
            _rectTransform.anchoredPosition = Vector2.Lerp(start, end, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        _rectTransform.anchoredPosition = end;
        callback?.Invoke();
    }
}
