using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    [SerializeField] private Image _image;

    private Coroutine _fillingCoroutine;
    private const float MaxFillAmount = 1f;

    public void StartCount(int time)
    {
        if (_fillingCoroutine != null)
            StopCoroutine(_fillingCoroutine);

        _fillingCoroutine = StartCoroutine(Filling(time, MaxFillAmount));
    }

    public void StopCount()
    {
        if(_fillingCoroutine != null)
            StopCoroutine(_fillingCoroutine);

        _fillingCoroutine = StartCoroutine(Filling(0.2f, 0));
    }

    private IEnumerator Filling(float time, float targetValue)
    {
        float elapsedTime = 0;
        float startValue = _image.fillAmount;

        while (elapsedTime < time)
        {
            _image.fillAmount = Mathf.Lerp(startValue, targetValue, elapsedTime / time);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _image.fillAmount = targetValue;
    }
}
