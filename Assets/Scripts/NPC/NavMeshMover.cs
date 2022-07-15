using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _endPoint;

    private void Awake()
    {
        _navMeshAgent.stoppingDistance = 2f;        
    }

    private void Update()
    {
        _navMeshAgent.destination = _endPoint.position;

    }
}
