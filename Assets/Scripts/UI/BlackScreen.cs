using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    [SerializeField] private Image _screen;
    [SerializeField] private float _duration;

    private void Start()
    {
        _screen.DOFade(0, _duration);
    }
}
