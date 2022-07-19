using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int _points;
    [SerializeField] private TeamId _teamId;
    [SerializeField] private bool _isConnected;

    private readonly int _maxPoints = 100;
    private int _connectionCounter;
    private Team _capturingTeam;

    public TeamId TeamId => _teamId;
    public bool IsConnected => _isConnected;

    public event UnityAction<int> PointsChanged;
    public event UnityAction<Color> ColorChanged;

    private void Start()
    {
        PointsChanged?.Invoke(_points);
    }

    public void TryCapture(Team team)
    {
        _capturingTeam = team;

        if (_teamId != _capturingTeam.TeamId)
        {
            _isConnected = true;
            _connectionCounter++;
            StartCoroutine(Capturing());
        }
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
        
        StartCoroutine(Increasing());
    }
}
