using System.Collections;
using NodeCanvas.BehaviourTrees;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] private BehaviourTreeOwner _behaviour;

    public void EnableBihaviour()
    {
        _behaviour.enabled = true;
    }

    public void DisableBihaviour()
    {
        _behaviour.enabled = false;
    }
}
