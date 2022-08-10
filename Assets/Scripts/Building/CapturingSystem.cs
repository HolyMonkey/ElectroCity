using UnityEngine;
using System;

public class CapturingSystem
{
    private const int _maxPoints = 60;

    public int TotalPoints { get; private set; }
    public Team CurrentTeam { get; private set; }
    public int MaxPoints => _maxPoints;

    public event Action<int> PointsChanged;
    public event Action<Color> PointsAdded;
    //public event Action<Color, float, float> PointsAdded;
    public event Action<Team> TeamChanged;

    public void Init(Team team, int initialPoints)
    {
        CurrentTeam = team;
        TotalPoints = initialPoints;

        PointsChanged?.Invoke(TotalPoints);
        //TeamChanged?.Invoke(CurrentTeam);
        //PointsAdded?.Invoke(CurrentTeam.Color, TotalPoints, _maxPoints);
        PointsAdded?.Invoke(CurrentTeam.Color);
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

    public void IncreseEnergy(int value)
    {
        ChangePoints(value);
    }

    private void ChangeTeamPoints(int value, Team team)
    {
        ChangePoints(value);

        if (TotalPoints <= 0)
        {
            CurrentTeam.OnBuildingCaptured(team, value);
            ChangeTeam(team);
            ChangePoints(1);
        }

        //PointsAdded?.Invoke(CurrentTeam.Color, TotalPoints, _maxPoints);
        PointsAdded?.Invoke(CurrentTeam.Color);
    }

    private void ChangePoints(int value)
    {
        TotalPoints += value;

        TotalPoints = Mathf.Clamp(TotalPoints, 0, _maxPoints);

        PointsChanged?.Invoke(TotalPoints);
    }

    private void ChangeTeam(Team team)
    {
        CurrentTeam = team;

        TeamChanged?.Invoke(team);

        if(team.TeamId == TeamId.First)
        SoundHandler.Instance.PlayCapturingSound();
    }
}
