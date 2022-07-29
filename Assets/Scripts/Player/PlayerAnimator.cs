using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private string _currentState;

    private const string Dancing = "Dancing";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void TryChangeStateTo(string newState)
    {
        if (_currentState == newState)
            return;

        _animator.Play(newState);
        _currentState = newState;
    }

    public void StartDancing()
    {
        _animator.Play(Dancing);
    }
}
