using UnityEngine;
using Obi;

public class RopeHandler : MonoBehaviour
{
    [SerializeField] private Transform _ropePoint;
    [SerializeField] private Rope _currentRope;
    [SerializeField] private ObiRigidbody _obiRigidbody;

    private bool _hasRope;

    public Transform RopePoint => _ropePoint;

    public Rope CurrentRope => _currentRope;

    public bool HasRope => _hasRope;

    public void TakeRope(Rope rope)
    {
        _currentRope = rope;
        _hasRope = true;
    }

    public void PlaceRope(Transform setPoint)
    {
        _currentRope.EndPoint.SetParent(setPoint);
        _currentRope.EndPoint.localPosition = Vector3.zero;
        _currentRope.ObiRope.tearingEnabled = false;
        _currentRope = null;
        _hasRope = false;
    }
}
