using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuildingShaker : MonoBehaviour
{
    [SerializeField] private Building _building;
    [Header("Animation Settings")]
    [SerializeField] private float _duration;
    [SerializeField] private float _strength;
    [SerializeField] private int _vibrato;
    [SerializeField] private float _randomness;

    private void OnEnable()
    {
        _building.CapturingSystem.PointsChanged += StartAnimation;
    }

    private void OnDisable()
    {
        _building.CapturingSystem.PointsChanged -= StartAnimation;
    }

    private void StartAnimation(int value = 0)
    {
        transform.DOShakeScale(_duration, new Vector3(_strength,0, _strength), _vibrato, _randomness);
    }
}
