using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private const string Run = "Run";

    public void TriggerRun()
    {
        _animator.SetTrigger(Run);
    }

    public void ResetRun()
    {
        _animator.ResetTrigger(Run);
    }
}
