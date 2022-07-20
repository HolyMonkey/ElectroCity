using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int _teamPoints;
    [SerializeField, Range(0, 100)] private int _neutralPoints;
    [SerializeField] private TeamId _teamId;
    //[SerializeField] private bool _isConnected;
    [SerializeField] private List<Rope> _pickedRopes;
    [SerializeField] private List<Rope> _settedRopes;

    private readonly int _maxPoints = 100;
    private int _maxPickUpedRopes = 3;
    private int _pickUpedRopes;
    private Team _capturingTeam;
    private Team _leadTeam;
    private bool _isNeutral = true;

    public TeamId TeamId => _teamId;
    //public bool IsConnected => _isConnected;
    public int PickUpedRopes => _pickUpedRopes;
    public int MaxPickUpedRopes => _maxPickUpedRopes;
    public int TeamPoints => _teamPoints;
    public int NeutralPoints => _neutralPoints;

    public event Action<int> PointsChanged;
    public event Action<Color, float, float> PointsAdded;

    private void Start()
    {
        PointsChanged?.Invoke(_neutralPoints);
    }

    private void Update()
    {
        CheckRopes();

        foreach(var rope in _settedRopes)
        {
            print($" rope {_settedRopes.IndexOf(rope)} {rope.IsConnected}");
        }
    }

    public void SetNeutralTeam(Team team)
    {
        _capturingTeam = team;
        _capturingTeam.AddPoints(_neutralPoints);
    }

    public void TryCapture(Team team, Rope rope)
    {
        _capturingTeam = team;

        if (_teamId != _capturingTeam.TeamId)
        {
            //_isConnected = true;
            rope.Connect();

            if (_isNeutral)
            {
                StartCoroutine(TryCapturingNeutral(rope));
            }
            else
            {
                StartCoroutine(TryCapturingTeam(rope));
            }
        }
    }

    public void AddSetedRope(Rope rope)
    {
        _settedRopes.Add(rope);
    }

    public void AddPickedRope(Rope rope)
    {
        _pickedRopes.Add(rope);
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
        foreach (var rope in _pickedRopes)
        {
            if (rope != null && rope.IsTorn)
            {
                _pickUpedRopes--;
                _pickedRopes.Remove(rope);
                break;
            }
        }
    }

    private void TryDestroyOthersTeamsRopes()
    {
        foreach(var rope in _settedRopes)
        {
            if(rope.TeamId != _leadTeam.TeamId && rope != null)
            {
                rope.Disconnect();
                rope.Fall();
                //Destroy(rope.gameObject);
            }
        }
    }

    private IEnumerator TryCapturingTeam(Rope rope)
    {
        if(_teamId != _capturingTeam.TeamId)
        {
            while(_teamPoints > 0 && rope.IsConnected)
            {
                _teamPoints = ChangePoints(-1, _teamPoints);
                PointsAdded?.Invoke(_leadTeam.Color, _teamPoints, _maxPoints);
                yield return new WaitForSeconds(0.2f);
            }

            if (rope.IsConnected)
            {
                _teamId = _capturingTeam.TeamId;
                _leadTeam = _capturingTeam;
            }
        }

        while(rope.IsConnected && rope != null)
        {
            _teamPoints = ChangePoints(1, _teamPoints);
            PointsAdded?.Invoke(_leadTeam.Color, _teamPoints, _maxPoints);
            //_capturingTeam.AddPoints(1);

            if (_teamPoints >= _maxPoints)
            {
                TryDestroyOthersTeamsRopes();
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator TryCapturingNeutral(Rope rope)
    {
        while (_neutralPoints > 0 && rope.IsConnected)
        {
            _neutralPoints = ChangePoints(-1, _neutralPoints);
            yield return new WaitForSeconds(0.2f);
        }

        _teamId = _capturingTeam.TeamId;
        _leadTeam = _capturingTeam;
        _isNeutral = false;
        StartCoroutine(TryCapturingTeam(rope));
    }
}
