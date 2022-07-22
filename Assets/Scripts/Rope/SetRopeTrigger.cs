using System.Collections;
using UnityEngine;

public class SetRopeTrigger : MonoBehaviour
{
    [SerializeField] private Transform _connectPoint;
    [SerializeField] private Building _building;
    [SerializeField] private float _delay;

    private Team _team;
    private int _numberOfPlacements;
    private readonly int _maxNumberOfPlacements = 1;
    private int _settedRopeCounter;
    private float _startAngle = 90f;

    public bool IsTryingToPlaceTwice => _numberOfPlacements < _maxNumberOfPlacements;
    public TeamId TeamId => _building.TeamId;

    private void OnTriggerEnter(Collider other)
    {
        if (CanAttach(other, out RopeHandler handler))
        {
            StartCoroutine(Attaching(_delay, handler));
        }
    }

    public bool IsTryingPlaceTwice(TeamId teamId)
    {
        if (_building.AreRoesDestroyed)
        {
            _numberOfPlacements--;
        }

        if(_team == null)
        {
            return false;
        }

        if (teamId != _team.TeamId && _numberOfPlacements < _maxNumberOfPlacements)
        {
            _numberOfPlacements++;
            return false;
        }

        return true;
    }

    private IEnumerator Attaching(float delay, RopeHandler handler)
    {
        yield return new WaitForSeconds(delay);

        _team = handler.Team;
        _building.AddSetedRope(handler.CurrentRope);
        handler.PlaceRope(transform);
    }

    private bool CanAttach(Collider other, out RopeHandler handler)
    {
        return other.TryGetComponent(out handler) && handler.HasRope && !IsTryingPlaceTwice(handler.Team.TeamId) && handler.PickUpTrigger.Building != _building;
    }

    private Transform CreateNextAttachPoint()
    {
        GameObject newAttachPoint = Instantiate(new GameObject(), _connectPoint);
        _settedRopeCounter++;
        float angleStep = 180f / 6;
        float angle = _startAngle - angleStep * _settedRopeCounter;
        newAttachPoint.transform.localRotation *= Quaternion.Euler(0, angle, 0);
        newAttachPoint.transform.localPosition += newAttachPoint.transform.forward * _connectPoint.GetComponent<SphereCollider>().radius;

        return newAttachPoint.transform;
    }
}
