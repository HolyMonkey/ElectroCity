using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int _points;

    private bool _isConnected;
    private readonly int _maxPoints = 100;

    public UnityAction<int> PointsChanged;

    private void Start()
    {
        PointsChanged?.Invoke(_points);
    }

    public void IncreasePoints()
    {
        _isConnected = true;
        StartCoroutine(Increasing());
    }

    public void StopIncreasingPoints()
    {
        _isConnected = false;
    }

    private void AddPoint()
    {
        _points++;
        _points = Mathf.Clamp(_points, 0, _maxPoints);
        PointsChanged?.Invoke(_points);
    }

    private IEnumerator Increasing()
    {
        while(_points <= _maxPoints && _isConnected)
        {
            AddPoint();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
