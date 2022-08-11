using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private RopeHandler _ropeHandler;

    private const string Run = "Run";
    private const string RopeRun = "RopeRun";
    private const string Idle = "Idle";

    public void TriggerRun()
    {
        if (_ropeHandler.HasRope == false)
            _animator.SetTrigger(Run);
        else
            _animator.SetTrigger(RopeRun);
    }

    public void TriggerIdle()
    {
        ResetRun();
        _animator.SetTrigger(Idle);
    }

    public void ResetRun()
    {
        _animator.ResetTrigger(Run);
        _animator.ResetTrigger(RopeRun);
    }
}
