using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int _teamPoints;
    [SerializeField, Range(0, 100)] private int _neutralPoints;
    [SerializeField] private TeamId _teamId;
    [SerializeField] private bool _isConnected;
    [SerializeField] private List<RopePickUpTrigger> _pickUpTriggers;
    [SerializeField] private List<Rope> _ropes;

    private readonly int _maxPoints = 100;
    private int _maxPickUpedRopes = 3;
    private int _pickUpedRopes;
    private Team _capturingTeam;
    private bool _isNeutral = true;

    public TeamId TeamId => _teamId;
    public bool IsConnected => _isConnected;
    public int PickUpedRopes => _pickUpedRopes;
    public int MaxPickUpedRopes => _maxPickUpedRopes;

    public event Action<int> PointsChanged;
    public event Action<Color> ColorChanged;
    public event Action<Color, float, float> PointsAdded;

    private void Start()
    {
        PointsChanged?.Invoke(_neutralPoints);
    }

    private void Update()
    {
        CheckRopes();
    }

    private void OnEnable()
    {
        foreach(var trigger in _pickUpTriggers)
        {
            trigger.RopeTaken += OnRopeTaken;
        }
    }

    private void OnDisable()
    {
        foreach (var trigger in _pickUpTriggers)
        {
            trigger.RopeTaken -= OnRopeTaken;
        }
    }

    public void TryCapture(Team team)
    {
        _capturingTeam = team;

        if (_teamId != _capturingTeam.TeamId)
        {
            _isConnected = true;

            if (_isNeutral)
            {
                StartCoroutine(TryCapturingNeutral());
            }
            else
            {
                StartCoroutine(TryCapturingTeam());
            }
        }
    }

    private void OnRopeTaken(Rope rope)
    {
        _ropes.Add(rope);
        _pickUpedRopes++;
    }

    private int ChangePoints(int value, int points)
    {
        points += value;
        points = Mathf.Clamp(points, 0, _maxPoints);
        PointsChanged?.Invoke(points);

        return points;
    }

    private void CheckRopes()
    {
        foreach (var rope in _ropes)
        {
            if (rope != null && rope.IsTorn)
            {
                _pickUpedRopes--;
                _ropes.Remove(rope);
                break;
            }
        }
    }

    private IEnumerator TryCapturingTeam()
    {
        if(_teamId != _capturingTeam.TeamId)
        {
            while(_teamPoints > 0 && _isConnected)
            {
                _teamPoints = ChangePoints(-1, _teamPoints);
                PointsAdded?.Invoke(_capturingTeam.Color, _teamPoints, _maxPoints);
                yield return new WaitForSeconds(0.2f);
            }

            _teamId = _capturingTeam.TeamId;
        }

        while(_isConnected)
        {
            _teamPoints = ChangePoints(1, _teamPoints);
            PointsAdded?.Invoke(_capturingTeam.Color, _teamPoints, _maxPoints);
            _capturingTeam.AddPoints(1);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator TryCapturingNeutral()
    {
        while (_neutralPoints > 0 && _isConnected)
        {
            _neutralPoints = ChangePoints(-1, _neutralPoints);
            yield return new WaitForSeconds(0.2f);
        }

        _teamId = _capturingTeam.TeamId;
        _isNeutral = false;
        //ColorChanged?.Invoke(_capturingTeam.Color);
        StartCoroutine(TryCapturingTeam());
    }
}
