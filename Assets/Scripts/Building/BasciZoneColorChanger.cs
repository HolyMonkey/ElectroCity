using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasciZoneColorChanger : MonoBehaviour
{
    [SerializeField] private Building _building;
    [SerializeField] private ParticleSystem _zone;

    [System.Obsolete]
    private void Start()
    {
        ChangeColor(_building.CapturingSystem.CurrentTeam);
    }

    [System.Obsolete]
    private void OnEnable()
    {
        _building.CapturingSystem.TeamChanged += ChangeColor;

    }

    [System.Obsolete]
    private void OnDisable()
    {
        _building.CapturingSystem.TeamChanged -= ChangeColor;
    }

    [System.Obsolete]
    private void ChangeColor(Team team)
    {
        _zone.startColor = team.Color;
    }
}
