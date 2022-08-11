using System.Collections;
using UnityEngine;

public class SetRopeTrigger : MonoBehaviour
{
    [SerializeField] private Transform _connectPoint;
    [SerializeField] private Building _building;
    [SerializeField] private float _delay;
    [SerializeField] private Transform _refrenceObject;
    [SerializeField] private RopePickUpTrigger _ropePickUpTrigger;

    private Rope _currentRope;
    private Team _team;
    private int _numberOfPlacements;
    private Coroutine _attachingCoroutine;
    private Coroutine _takingCoroutine;
    private readonly int _maxNumberOfPlacements = 1;
    public bool IsAttaching;
    private SetRopHandler _setRopHandler;

    public bool IsTryingToPlaceTwice => _numberOfPlacements < _maxNumberOfPlacements;
    public bool IsFree => _currentRope == null;
    public TeamId TeamId => _building.TeamId;
    public Building Building => _building;

    private void OnTriggerEnter(Collider other)
    {
        if (CanAttach(other, out RopeHandler handler))
        {
            Attach(handler);
        }

        if (other.TryGetComponent(out RopeHandler ropeHandler) && IsAttaching == false && ropeHandler.IsBot == false && IsFree == false && ropeHandler.HasRope == false && ropeHandler.Team.TeamId == TeamId.First)
        {
            _setRopHandler.Pick(this);
            _setRopHandler.UnTakeExcept(this, ropeHandler);
            TakeRope(ropeHandler);
        }

        if (other.TryGetComponent(out Player player) && IsFree == false && _currentRope.TeamId != TeamId.First)
        {
            player.RopeHandler.BreakeEnemyRope(_currentRope, this);
        }
        else if(other.TryGetComponent(out Player player1) && _building.CapturingSystem.CurrentTeam.TeamId != TeamId.First)
        {
            StartCoroutine(StartingJumpingDown(player1));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out RopeHandler ropeHandler))
        { 
            UntakeRope(ropeHandler);
        }
    }

    public void Init(SetRopHandler setRopHandler)
    {
        _setRopHandler = setRopHandler;
    }

    public void Attach(RopeHandler handler)
    {
        if(handler.CurrentRope != null && IsFree)
        {
            _attachingCoroutine = StartCoroutine(Attaching(_delay, handler));
        }
    }

    public void TakeRope(RopeHandler handler)
    {
        if (_currentRope == null)
            return;


        _takingCoroutine = StartCoroutine(TakingRope(handler));
        _currentRope.Plug.Raise();
        _ropePickUpTrigger.StopTaking();
    }

    public void UntakeRope(RopeHandler handler)
    {
        IsAttaching = false;

        if(_takingCoroutine != null)
            StopCoroutine(_takingCoroutine);

        if(_currentRope!= null)
            _currentRope.Plug.Set();

        if (_attachingCoroutine != null)
            StopCoroutine(_attachingCoroutine);

        _ropePickUpTrigger.TryAttach(handler);
    }

    private IEnumerator TakingRope(RopeHandler handler)
    {
        yield return new WaitForSeconds(0.6f);

        if (_attachingCoroutine != null)
            StopCoroutine(_attachingCoroutine);

        handler.TakeRope(_currentRope);
        DeleteRope(_currentRope);
    }

    private void DeleteRope(Rope rope)
    {
        if (_currentRope == null)
            return;

        _currentRope.Torned -= DeleteRope;
        _currentRope.Disconnect(false);
        _building.OnRopeRemoved(_currentRope);
        _currentRope = null;
    }

    private IEnumerator Attaching(float delay, RopeHandler handler)
    {

        IsAttaching = true;
        yield return new WaitForSeconds(delay);

        if (handler.CurrentRope!= null && IsFree)
            ForceAttach(handler);

        IsAttaching = false;
    }

    public void ForceAttach(RopeHandler handler)
    {
        if (handler.CurrentRope.Building == _building)
            return;

        _currentRope = handler.CurrentRope;
        _currentRope.Torned += DeleteRope;
        _team = handler.Team;
        _building.AddSetedRope(handler.CurrentRope);
        handler.PlaceRope(_connectPoint, _refrenceObject.transform.localRotation);
        IsAttaching = false;
    }

    public bool CanAttach(Collider other, out RopeHandler handler)
    {
        return other.TryGetComponent(out handler) && handler.HasRope && IsFree && handler.CurrentRope.Building != _building;
    }

    private IEnumerator StartingJumpingDown(Player player)
    {
        player.Mover.Disable();
        player.Animator.StartJumpingDown();

        yield return new WaitForSeconds(1f);

        player.Mover.Enable();
    }
}
