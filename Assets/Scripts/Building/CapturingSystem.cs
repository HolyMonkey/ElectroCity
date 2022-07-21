using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CapturingSystem
{
    private const int _maxPoints = 100;
    private bool _canChangeTeam = true;

    public int TotalPoints { get; private set; }
    public Team CurrentTeam { get; private set; }

    public event Action<int> PointsChanged;
    public event Action<Color, float, float> PointsAdded;
    public event Action<Team> TeamChanged;

    public void Init(Team team, int initialPoints)
    {
        CurrentTeam = team;
        TotalPoints = initialPoints;

        PointsChanged?.Invoke(TotalPoints);
    }

    public void TakeEnergy(int value, Team team)
    {
        if (team.TeamId == CurrentTeam.TeamId)
            ChangePoints(value, team);
        else
            ChangePoints(-value, team);
    }

    private void ChangePoints(int value, Team team)
    {
        TotalPoints += value;

        TotalPoints = Mathf.Clamp(TotalPoints, 0, _maxPoints);

        PointsChanged?.Invoke(TotalPoints);
        PointsAdded?.Invoke(CurrentTeam.Color, TotalPoints, _maxPoints);

        if (TotalPoints <= 0)
            ChangeTeam(team);

    }

    private void ChangeTeam(Team team)
    {
        if(_canChangeTeam)
            CurrentTeam = team;

        //TeamChanged?.Invoke(team);
    }

}
