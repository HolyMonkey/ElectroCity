using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    [SerializeField] private Color32 _color;
    [SerializeField] private TeamId _teamId;
    [SerializeField] private SkinnedMeshRenderer _stickmanMesh;

    private int _totalAmount;

    public float Points { get; private set; }
    public Material StickmanMaterial => _stickmanMesh.material;

    public TeamId TeamId => _teamId;
    public Color32 Color => _color;

    public event Action PointsChanged;
    public event Action<Team> Lost;

    public void AddPoints(int amount)
    {
        ChangePoints(amount);
    }

    public void TakePoints(int amount, out int points)
    {
        points = amount;

        if (Points - amount <= 0)
        {
            points = (int)Points;
        }

        ChangePoints(-points);
    }

    public void ChangePoints(int amount)
    {
        Points += amount;
        Points = Mathf.Clamp(Points, 0, 500f);
    }

    public void SetTotalAmount(int totalAmount)
    {
        _totalAmount = totalAmount;
    }

    public void SetPoint(int amount)
    {
        Points = amount;
    }

    public void OnBuildingCaptured(Team teamThatCapture, int value)
    {
        if (Points <= 2)
            OnTeamLost(teamThatCapture);
    }

    private void OnTeamLost(Team teamThatCapture)
    {
        Lost?.Invoke(this);

        var handlers = FindObjectsOfType<RopeHandler>();

        foreach (var handler in handlers)
        {
            if (handler.Team.TeamId == TeamId && handler.Team != this && handler.TryGetComponent(out Team team))
                team.ChangeTeamData(teamThatCapture);

        }

        ChangeTeamData(teamThatCapture);
    }

    public void ChangeTeamData(Team teamThatCapture)
    {
        TeamId initialTeamId = _teamId;
        _color = teamThatCapture.Color;
        _teamId = teamThatCapture.TeamId;
        Points = teamThatCapture.Points;

        if (_stickmanMesh != null && initialTeamId != TeamId.First)
            _stickmanMesh.material = teamThatCapture.StickmanMaterial;
    }
}
