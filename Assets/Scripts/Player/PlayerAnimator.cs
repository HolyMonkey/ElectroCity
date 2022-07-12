using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private const string Idle = "Idle";
    private const string Running = "Running";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetIdle()
    {
        _animator.SetTrigger(Idle);
    }

    public void SetRun()
    {
        _animator.SetTrigger(Running);
    }
}
