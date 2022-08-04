using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private string _currentState;

    private const string Idle = "Idle";
    private const string Running = "Running";
    private const string Dancing = "Dancing";
    private const string Shock = "Shock";
    private const string Falling = "Falling";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartRunning()
    {
        TryChangeStateTo(Running);
    }

    public void StartIdling()
    {
        TryChangeStateTo(Idle);
    }

    public void StartDancing()
    {
        TryChangeStateTo(Dancing);
    }

    public void StartShock()
    {
        TryChangeStateTo(Shock);
    }

    public void StartFalling()
    {
        TryChangeStateTo(Falling);
    }

    private void TryChangeStateTo(string newState)
    {
        if (_currentState != newState)
        {
            _animator.Play(newState);
            _currentState = newState;
        }
    }
}
