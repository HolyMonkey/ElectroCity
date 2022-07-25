using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Teams : MonoBehaviour
{
    [SerializeField] private Team[] _teams;
    [SerializeField] private MultiColorSlider _multiColorSlider;

    private bool isSwaped;
    private Building[] _buildings;
    private int _elapsedFrames;

    private void Awake()
    {
        _multiColorSlider.CreateBlank();
        _buildings = FindObjectsOfType<Building>();
    }

    private void Update()
    {
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


        if (_elapsedFrames > 2)
        {
            ReColor();
            _elapsedFrames = 0;
        }
    }

    private void ReColor()
    {
        _multiColorSlider.Colorize(_teams);
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
