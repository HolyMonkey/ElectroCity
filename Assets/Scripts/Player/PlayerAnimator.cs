using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private string _currentState;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeState(string newState)
    {
        if (_currentState == newState)
            return;

        _animator.Play(newState);
        _currentState = newState;
    }
}
