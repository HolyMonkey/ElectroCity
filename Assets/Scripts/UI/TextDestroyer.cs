using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class TextDestroyer : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);

        _background.DOFade(0, _fadeDuration);
        _text.DOFade(0, _fadeDuration);
    }
}
