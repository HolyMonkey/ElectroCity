using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private string _currentState;

    private const string Idle = "Idle";
    private const string Running = "Running";
    private const string Dancing = "Dancing";

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

    private void TryChangeStateTo(string newState)
    {
        if (_currentState != newState)
        {
            _animator.Play(newState);
            _currentState = newState;
        }
    }
}
