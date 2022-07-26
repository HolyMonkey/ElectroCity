using System.Collections;
using UnityEngine;

public class SetRopeTrigger : MonoBehaviour
{
    [SerializeField] private Transform _connectPoint;
    [SerializeField] private Building _building;
    [SerializeField] private float _delay;
    [SerializeField] private Transform _refrenceObject;

    private Rope _currentRope;
    private Team _team;
    private int _numberOfPlacements;
    private readonly int _maxNumberOfPlacements = 1;
    private int _settedRopeCounter;
    private float _startAngle = 90f;

    public bool IsTryingToPlaceTwice => _numberOfPlacements < _maxNumberOfPlacements;
    public bool IsFree => _currentRope == null;
    public TeamId TeamId => _building.TeamId;

    private void OnTriggerEnter(Collider other)
    {
        //if (CanAttach(other, out RopeHandler handler))
        //{
        //    StartCoroutine(Attaching(_delay, handler));
        //}

        //if (other.TryGetComponent(out RopeHandler ropeHandler) && IsFree == false && !ropeHandler.HasRope)
        //{
        //    TakeRope(ropeHandler);
        //}
    }

    public void Attach(RopeHandler handler)
    {
        if(handler.CurrentRope != null)
        {
            StartCoroutine(Attaching(_delay, handler));
        }
    }

    public void TakeRope(RopeHandler handler)
    {
        handler.TakeRope(_currentRope);
        _building.OnRopeRemoved(_currentRope);
        DeleteRope(_currentRope);
    }


    public bool IsTryingPlaceTwice(TeamId teamId)
    {
        //if (_building.AreRoesDestroyed)
        //{
        //    _numberOfPlacements--;
        //}

        //if(_team == null)
        //{
        //    return false;
        //}

        //if (teamId != _team.TeamId && _numberOfPlacements < _maxNumberOfPlacements)
        //{
        //    _numberOfPlacements++;
        //    return false;
        //}

        return !IsFree;
    }

    private void DeleteRope(Rope rope)
    {
        rope.Torned -= DeleteRope;
        _currentRope = null;
    }


    private IEnumerator Attaching(float delay, RopeHandler handler)
    {
        yield return new WaitForSeconds(delay);

        _team = handler.Team;
        _building.AddSetedRope(handler.CurrentRope);
        _currentRope = handler.CurrentRope;
        _currentRope.Torned += DeleteRope;
        handler.PlaceRope(_connectPoint, _refrenceObject.transform.localRotation);
    }

    public bool CanAttach(Collider other, out RopeHandler handler)
    {
        return other.TryGetComponent(out handler) && handler.HasRope && IsFree && handler.PickUpTrigger.Building != _building;
    }

    //private Transform CreateNextAttachPoint()
    //{
    //    GameObject newAttachPoint = Instantiate(new GameObject(), _connectPoint);
    //    _settedRopeCounter++;
    //    float angleStep = 180f / 6;
    //    float angle = _startAngle - angleStep * _settedRopeCounter;
    //    newAttachPoint.transform.localRotation *= Quaternion.Euler(0, angle, 0);
    //    newAttachPoint.transform.localPosition += newAttachPoint.transform.forward * _connectPoint.GetComponent<SphereCollider>().radius;

    //    return newAttachPoint.transform;
    //}
}
