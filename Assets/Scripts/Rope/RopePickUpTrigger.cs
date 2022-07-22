using System.Collections;
using UnityEngine;
using System;

public class RopePickUpTrigger : MonoBehaviour
{
    [SerializeField] private RopeSpawner _ropeSpawner;
    [SerializeField] private Building _building;
    [SerializeField] private float _delay;
    
    private bool _isPickingUp;
    private Coroutine _takingCoroutine;
    private RopeHandler _currentRopeHandler;

    public Building Building => _building;
    public bool IsThereFreeRope => _building.PickUpedRopesCount < _building.MaxPickUpedRopes;
    public TeamId TeamId => _building.TeamId;

    private void OnTriggerEnter(Collider other)
    {
        if(CanTake(other, out RopeHandler handler))
        {
            _currentRopeHandler = handler;
            _takingCoroutine = StartCoroutine(Taking(_delay, handler));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out RopeHandler ropeHandler) && ropeHandler == _currentRopeHandler && _takingCoroutine != null)
        {
            StopCoroutine(_takingCoroutine);
            _currentRopeHandler = null;
            _isPickingUp = false;
        }
    }

    private IEnumerator Taking(float delay, RopeHandler handler)
    {
        _isPickingUp = true;

        yield return new WaitForSeconds(delay);

        handler.SetTrigger(this);
        _ropeSpawner.Spawn(handler);
        _building.AddPickedRope(handler.CurrentRope);
        _isPickingUp = false;
    }

    private bool CanTake(Collider other, out RopeHandler handler)
    {
        return other.TryGetComponent(out handler) && !handler.HasRope && !_isPickingUp &&
            handler.Team.TeamId == _building.TeamId && IsThereFreeRope;
    }
}
