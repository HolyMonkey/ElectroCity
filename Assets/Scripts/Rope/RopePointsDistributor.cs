using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RopePointsDistributor : MonoBehaviour
{
    [SerializeField] private List<RopeSpawner> _spawners;
    [SerializeField] private List<Transform> _points;

    private void Awake()
    {
        _spawners = FindObjectsOfType<RopeSpawner>().ToList();

        for(int i = 0; i < _spawners.Count; i++)
        {
            _spawners[i].SetRopePoint(_points[i]);
        }          
    }
}
