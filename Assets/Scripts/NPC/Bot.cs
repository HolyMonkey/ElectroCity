using System.Collections;
using NodeCanvas.BehaviourTrees;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BehaviourTreeOwner _behaviour;

    private const string Idle = "Idle";

    public void EnableBihaviour()
    {
        _behaviour.enabled = true;
    }

    public void DisableBihaviour()
    {
        _behaviour.enabled = false;
    }

    public void StartIdle()
    {
        _animator.Play(Idle);
        print("reached");
    }
}
