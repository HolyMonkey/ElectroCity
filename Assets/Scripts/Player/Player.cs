using UnityEngine;
using Obi;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _ropePoint;
    [SerializeField] private Rope _currentRope;

    private bool _hasRope;

    public Transform RopePoint => _ropePoint;

    public Rope CurrentRope => _currentRope;

    public bool HasRope => _hasRope;

    public void TakeRope(Rope rope)
    {
        _currentRope = rope;
        _hasRope = true;
    }

    public void SetRope(Transform setPoint)
    {
        _currentRope.EndPoint.SetParent(setPoint);
        _currentRope.EndPoint.localPosition = Vector3.zero;
        _currentRope = null;
        _hasRope = false;
    }
}
