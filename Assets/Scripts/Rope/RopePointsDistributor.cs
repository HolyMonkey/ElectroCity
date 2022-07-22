using System.Collections.Generic;
using UnityEngine;

public class RopePointsDistributor : MonoBehaviour
{
    [SerializeField] private RopeSpawner[] _spawners;
    [SerializeField] private List<Transform> _points;

    private void Awake()
    {
        _spawners = FindObjectsOfType<RopeSpawner>();

        for(int i = 0; i < _spawners.Length; i++)
        {
            _spawners[i].SetRopePoint(_points[i]);
        }          
    }
}
