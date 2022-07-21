using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCapture : MonoBehaviour
{
    [SerializeField] private Team _team;

    private Building[] _buildings;

    //private void Awake()
    //{
    //    _buildings = FindObjectsOfType<Building>();

    //    foreach (var building in _buildings)
    //    {
    //        if(building.TeamId == null)
    //            building.SetNeutralTeam(_team);
    //    }
    //}
}
