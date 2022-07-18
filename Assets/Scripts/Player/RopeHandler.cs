using UnityEngine;
using Obi;
using static Obi.ObiRope;
using System;

public class RopeHandler : MonoBehaviour
{
    [SerializeField] private Transform _ropePoint;
    [SerializeField] private Rope _currentRope;

    private bool _hasRope;

    public Transform RopePoint => _ropePoint;

    public Rope CurrentRope => _currentRope;

    public bool HasRope => _hasRope;

    public event Action RopeTaken;

    public void TakeRope(Rope rope)
    {
        _currentRope = rope;
        _currentRope.ObiRope.OnRopeTorn += BreakRope;
        _hasRope = true;
        RopeTaken?.Invoke();
        Debug.Log("h");
    }

    public void PlaceRope(Transform setPoint)
    {
        _currentRope.EndPoint.SetParent(setPoint);
        _currentRope.EndPoint.localPosition = Vector3.zero;
        _currentRope.ObiRope.tearingEnabled = false;
        _currentRope = null;
        _hasRope = false;
    }

    private void BreakRope(ObiRope obiRope, ObiRopeTornEventArgs tearInfo)
    {
        _currentRope.ObiRope.OnRopeTorn -= BreakRope;
        _currentRope = null;
        _hasRope = false;
    }
}
