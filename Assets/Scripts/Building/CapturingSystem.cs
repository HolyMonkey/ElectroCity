using UnityEngine;
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
        TeamChanged?.Invoke(CurrentTeam);
    }

    public void ApplyEnergy(int value, Team team)
    {
        if (team.TeamId == CurrentTeam.TeamId)
            ChangeTeamPoints(value, team);
        else
            ChangeTeamPoints(-value, team);
    }

    public void DecreaseEnergy(int value)
    {
        ChangePoints(-value);
    }

    private void ChangeTeamPoints(int value, Team team)
    {
        ChangePoints(value);

        PointsChanged?.Invoke(TotalPoints);
        PointsAdded?.Invoke(CurrentTeam.Color, TotalPoints, _maxPoints);

        if (TotalPoints <= 0)
        {
            ChangeTeam(team);
            TotalPoints++;
        }
    }

    private void ChangePoints(int value)
    {
        TotalPoints += value;

        TotalPoints = Mathf.Clamp(TotalPoints, 0, _maxPoints);
    }

    private void ChangeTeam(Team team)
    {
        if(_canChangeTeam)
            CurrentTeam = team;

        TeamChanged?.Invoke(team);
    }

}
