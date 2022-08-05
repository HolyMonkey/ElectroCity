using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Teams : MonoBehaviour
{
    [SerializeField] private List<Team> _teams;
    [SerializeField] private MultiColorSlider _multiColorSlider;

    private WinnerDecider _winnerDecider;
    private bool _gameOver;
    private Building[] _buildings;
    private int _elapsedFrames;
    private int _counter;

    private void Awake()
    {
        _multiColorSlider.CreateBlank();
        _buildings = FindObjectsOfType<Building>();
        _winnerDecider = FindObjectOfType<WinnerDecider>();
        _counter = _teams.Count;
    }

    private void OnEnable()
    {
        foreach (var team in _teams)
        {
            team.Lost += OnTeamLost;
        }
    }

    private void OnDisable()
    {
        foreach (var team in _teams)
        {
            team.Lost -= OnTeamLost;
        }
    }

    private void Update()
    {
        if (_gameOver)
            return;

        _elapsedFrames++;

        foreach (var team in _teams)
        {
            int TotalPoints = 0;

            foreach (var building in _buildings)
            {
                if (building.TeamId == team.TeamId)
                    TotalPoints += building.CapturingSystem.TotalPoints;
            }

            team.SetPoint(TotalPoints);
        }

        if (_elapsedFrames > 4)
        {
            ReColor();
            _elapsedFrames = 0;
        }

        IsAllBuildingCaptured();
    }

    private void ReColor()
    {
        _multiColorSlider.Colorize(_teams);
    }

    private void OnTeamLost(Team team)
    {
        if (_teams.Contains(team))
        {
            team.Lost -= OnTeamLost;
            _counter--;

            _teams.Remove(team);
        }

        

        if (_counter <= 1 || team.TeamId == TeamId.First)
        {
            _winnerDecider.EndGame(_teams[0]);
            _gameOver = true;
        }
    }

    private void IsAllBuildingCaptured()
    {
        if (_gameOver)
            return;

        var teamId = _buildings[0].TeamId;
        bool isAllEqual = true;

        foreach (var building in _buildings)
        {
            if (building.TeamId != teamId)
            {
                isAllEqual = false;

                return;
            }
        }

        _winnerDecider.EndGame(_teams[0]);
        _gameOver = true;
    }
}

public enum TeamId
{
    First,
    Second,
    Third,
    Fourth,
    Netural
}
