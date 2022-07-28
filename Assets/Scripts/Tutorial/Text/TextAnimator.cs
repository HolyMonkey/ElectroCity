using TMPro;
using DG.Tweening;
using UnityEngine;
using System.Collections;

public class TextAnimator : MonoBehaviour
{
	[SerializeField] private TMP_Text _value;
    [SerializeField] private float _targetSize;
    [SerializeField] private float _speed;

    private float _startSize;

    private void Start()
    {
        _startSize = _value.fontSize;
        StartCoroutine(Animating());
    }

    private IEnumerator Animating()
    {
        while (true)
        {
            while (_value.fontSize < _targetSize)
            {
                _value.fontSize = Mathf.MoveTowards(_value.fontSize, _targetSize, _speed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(0.2f);

            while (_value.fontSize > _startSize)
            {
                _value.fontSize = Mathf.MoveTowards(_value.fontSize, _startSize, _speed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
