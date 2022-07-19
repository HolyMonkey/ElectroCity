using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int _points;
    [SerializeField] private TeamId _teamId;
    [SerializeField] private bool _isCapturedByEnemy;
    [SerializeField] private bool _isCapturedByPlayer;
    [SerializeField] private bool _isConnected;

    private Team _capturingTeam;
    private int _connectionCounter;
    private readonly int _maxPoints = 100;

    public bool IsCapturedByPlayer => _isCapturedByPlayer;
    public bool IsConnected => _isConnected;

    public UnityAction<int> PointsChanged;
    public UnityAction<Color> ColorChanged;

    private void Start()
    {
        PointsChanged?.Invoke(_points);
    }

    public void TryCapture(Team team)
    {
        _capturingTeam = team;
        _isConnected = true;
        _connectionCounter++;

        if (_teamId != _capturingTeam.TeamId)
        {
            StartCoroutine(Capturing());
        }
    }

    public void StopIncreasingPoints()
    {
        _isConnected = false;
    }

    private void ChangePoints(int value)
    {
        _points += value;
        _points = Mathf.Clamp(_points, 0, _maxPoints);
        PointsChanged?.Invoke(_points);
    }

    private IEnumerator Increasing()
    {
        while(_isConnected)
        {
            ChangePoints(1);
            yield return new WaitForSeconds(0.2f/ _connectionCounter);
        }
    }

    private IEnumerator Capturing()
    {
        while (_points > 0 && _isConnected)
        {
            ChangePoints(-1);
            yield return new WaitForSeconds(0.2f/ _connectionCounter);
        }

        _teamId = _capturingTeam.TeamId;
        ColorChanged?.Invoke(_capturingTeam.Color);
        _isCapturedByPlayer = true;
        StartCoroutine(Increasing());
    }
}
