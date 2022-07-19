using System.Collections;
using UnityEngine;

public class SetRopeTrigger : MonoBehaviour
{
    [SerializeField] private Transform _connectPoint;
    [SerializeField] private Building _building;
    [SerializeField] private float _delay;

    private Team _team;
    private int _numberOfPlacements;
    private readonly int _counter = 1;

    public bool IsConnected => _building.IsConnected;

    private void OnTriggerEnter(Collider other)
    {
        if (CanAttach(other, out RopeHandler handler))
        {
            StartCoroutine(Attaching(_delay, handler));
        }
    }

    private IEnumerator Attaching(float delay, RopeHandler handler)
    {
        yield return new WaitForSeconds(delay);

        _team = handler.Team;
        handler.PlaceRope(_connectPoint);
        _building.TryCapture(_team);
    }

    private bool CanAttach(Collider other, out RopeHandler handler)
    {
        return other.TryGetComponent(out handler) && handler.HasRope && !IsTryingPlaceTwice(handler) && handler.PickUpTrigger.Building != _building;
    }

    private bool IsTryingPlaceTwice(RopeHandler handler)
    {
        if (_team == null)
        {
            _numberOfPlacements++;
            return false;
        }
        else if (handler.Team.TeamId != _team.TeamId || _numberOfPlacements < _counter)
        {
            _numberOfPlacements++;
            return false;
        }

        return true;
    }
}
