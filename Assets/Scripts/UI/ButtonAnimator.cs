using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class ButtonAnimator : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _duration;
    [SerializeField] private float _scaleModifier;

    private Vector3 _currentScale;
    private Vector3 _startScale;

    private void Awake()
    {
        _startScale = _image.rectTransform.localScale;
        _currentScale = _startScale;    
    }

    private void Start()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        sequence.Append(_image.rectTransform.DOScale(_currentScale * _scaleModifier, _duration));
        sequence.Append(_image.rectTransform.DOScale(_startScale, _duration));
    }
}
