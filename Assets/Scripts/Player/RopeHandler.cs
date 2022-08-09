using UnityEngine;
using Obi;
using static Obi.ObiRope;
using System;
using System.Collections;

public class RopeHandler : MonoBehaviour
{
    [SerializeField] private Team _team;
    [SerializeField] private Transform _ropePoint;
    [SerializeField] private bool _disableTearing;

    private Rope _currentRope;
    private RopePickUpTrigger _pickUpTrigger;
    private bool _hasRope;

    public bool IsBot { get; private set; }
    public RopePickUpTrigger PickUpTrigger => _pickUpTrigger;
    public Team Team => _team;
    public Transform RopePoint => _ropePoint;
    public Rope CurrentRope => _currentRope;
    public bool HasRope => _hasRope;

    public event Action RopeTaken;
    public event Action RopeBreaked;
    public event Action EnemyRopeBreaked;

    private void Awake()
    {
        if (_team.TeamId != TeamId.First)
            IsBot = true;
    }

    private void Update()
    {
        //if (_currentRope == null)
        //    return;

        //if (_currentRope.ObiRope.CalculateLength() > 15f)
        //{
        //    _currentRope.Disconnect();
        //}
    }

    public void SetTrigger(RopePickUpTrigger pickUpTrigger)
    {
        _pickUpTrigger = pickUpTrigger;
    }

    public void TakeRope(Rope rope)
    {
        if (rope == null)
            return;

        _currentRope = rope;
        _currentRope.ObiRope.OnRopeTorn += BreakRope;
        _hasRope = true;
        rope.EndPoint.SetParent(_ropePoint);
        rope.StartPoint.localPosition = Vector3.zero;
        rope.EndPoint.localPosition = Vector3.zero;
        rope.Plug.SetHandRotation();
        RopeTaken?.Invoke();

        //if(rope.TeamId != TeamId.First && Team.TeamId == TeamId.First)
        //{
            
        //}
    }

    public void BreakeEnemyRope(Rope rope)
    {
        rope.EndPoint.SetParent(_ropePoint);
        rope.StartPoint.localPosition = Vector3.zero;
        rope.EndPoint.localPosition = Vector3.zero;
        rope.Plug.SetHandRotation();
        StartCoroutine(DelayBeforeBreaking(rope));
    }

    public void PlaceRope(Transform setPoint, Quaternion refernceObjectRotation)
    {
        if(_currentRope != null)
        {
            _currentRope.EndPoint.rotation = refernceObjectRotation;
            _currentRope.Plug.SetSocketRotatation();
            _currentRope.ObiRope.OnRopeTorn -= BreakRope;
            _currentRope.EndPoint.transform.localScale = Vector3.one * 2;
            _currentRope.EndPoint.SetParent(setPoint,true);
            _currentRope.EndPoint.localPosition = Vector3.zero;
            _currentRope.ObiRope.tearingEnabled = false;
            _currentRope = null;
            _hasRope = false;
        }
    }

    private void BreakRope(ObiRope obiRope, ObiRopeTornEventArgs tearInfos)
    {
        _hasRope = false;
        RopeBreaked?.Invoke();

        if(_currentRope != null)
            _currentRope.ObiRope.OnRopeTorn -= BreakRope;

        _currentRope = null;
    }

    private IEnumerator DelayBeforeBreaking(Rope rope)
    {
        yield return new WaitForSeconds(0.2f);

        EnemyRopeBreaked?.Invoke();
        rope.Disconnect();
    }

    private IEnumerator Delay(Rope rope)
    {
        rope.ObiRope.tearingEnabled = false;

        yield return new WaitForSeconds(1f);

        if(_disableTearing == false)
            rope.ObiRope.tearingEnabled = true;
    }
}
