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
    private Rope _cachedRope;

    public Building Building => _building;
    public bool IsThereFreeRope => _building.PickUpedRopesCount < _building.MaxPickUpedRopes;
    public TeamId TeamId => _building.TeamId;

    private void OnEnable()
    {
        _building.CapturingSystem.TeamChanged += ResetTaking;
    }

    private void OnDisable()
    {
        _building.CapturingSystem.TeamChanged -= ResetTaking;
    }

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
        if (other.TryGetComponent(out RopeHandler ropeHandler) && ropeHandler == _currentRopeHandler && _takingCoroutine != null)
        {
            StopTaking();

            _currentRopeHandler = null;

            if (_cachedRope != null && ropeHandler.HasRope == false && _cachedRope.IsConnected == false)
                RopeFlyBack();
        }
    }

    public void StopTaking()
    {
        if(_takingCoroutine!= null)
        {
            StopCoroutine(_takingCoroutine);
            _isPickingUp = false;
            RopeFlyBack();
        }
    }

    public bool TryAttach(RopeHandler ropeHandler)
    {
        if (CanAttach(ropeHandler))
        {
            _currentRopeHandler = ropeHandler;
            _takingCoroutine = StartCoroutine(Taking(_delay, ropeHandler));

            return true;
        }

        return false;
    }

    private void ResetTaking(Team team)
    {
        StopTaking();

        var colliders = Physics.OverlapSphere(transform.position, GetComponent<SphereCollider>().radius);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out RopeHandler ropeHandler))
            {
                TryAttach(ropeHandler);

                //Taking(0, ropeHandler);

                return;
            }
        }
    }

    public void TakeTutor(float delay, RopeHandler handler)
    {
        StartCoroutine(Taking(delay, handler, true));
    }

    private IEnumerator Taking(float delay, RopeHandler handler, bool isTeleport = false)
    {
        _isPickingUp = true;

        //if (_cachedRope != null)
        //    delay = 0;

        yield return new WaitForSeconds(delay);

        if(handler.HasRope == false)
        {
            handler.SetTrigger(this);

            _cachedRope = _ropeSpawner.Spawn(handler);

            _cachedRope.Plug.FlyTo(handler.RopePoint, OnRopeFlyEnd, isTeleport);
        }

        _isPickingUp = false;
    }

    public void RopeFlyBack()
    {
        if (_cachedRope != null)
            _cachedRope.Plug.FlyTo(transform, _cachedRope.Disable);
    }

    public void SetCurrentRopeHandler(RopeHandler handler)
    {
        _currentRopeHandler = handler;
    }

    private void OnRopeFlyEnd()
    {
        if (_currentRopeHandler.HasRope)
        {
            RopeFlyBack();

            return;
        }

        _ropeSpawner.AttachRope();
        _building.AddPickedRope(_currentRopeHandler.CurrentRope);
        _cachedRope = null;
    }

    private bool CanTake(Collider other, out RopeHandler handler)
    {
        return other.TryGetComponent(out handler) && CanAttach(handler);
    }

    private bool CanAttach(RopeHandler handler)
    {
        return !handler.HasRope && !_isPickingUp && handler.Team.TeamId == _building.TeamId && IsThereFreeRope;
    }
}
