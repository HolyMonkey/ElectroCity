using UnityEngine;
using Obi;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _ropePoint;
    [SerializeField] private Rope _currentRope;
    [SerializeField] private ObiRigidbody _obiRigidbody;

    private bool _hasRope;

    public Transform RopePoint => _ropePoint;

    public Rope CurrentRope => _currentRope;

    public bool HasRope => _hasRope;

    private void Update()
    {
        if (_currentRope != null && _currentRope.ObiRope.CalculateLength() >= 15f )
        {
            _currentRope.ObiRope.stretchingScale = 3f;
            _obiRigidbody.kinematicForParticles = false;
        }

        if (_currentRope == null)
            _obiRigidbody.kinematicForParticles = true;
    }

    public void TakeRope(Rope rope)
    {
        _currentRope = rope;
        _hasRope = true;
    }

    public void PlaceRope(Transform setPoint)
    {
        _currentRope.EndPoint.SetParent(setPoint);
        _currentRope.EndPoint.localPosition = Vector3.zero;
        _currentRope = null;
        _hasRope = false;
    }
}
