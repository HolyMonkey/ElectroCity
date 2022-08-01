using TMPro;
using DG.Tweening;
using UnityEngine;
using System.Collections;

public class TextAnimator : MonoBehaviour
{
    [SerializeField] private float _duration;

    public void DoRotation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(new Vector3(0, 0, 4f), _duration, RotateMode.Fast));
        sequence.Append(transform.DORotate(new Vector3(0, 0, -3f), _duration, RotateMode.Fast));
        sequence.Append(transform.DORotate(new Vector3(0, 0, 2f), _duration, RotateMode.Fast));
        sequence.Append(transform.DORotate(new Vector3(0, 0, 0f), _duration, RotateMode.Fast));
    }
}
