using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private int _points;
    [SerializeField] private bool _isNeutral;
    [SerializeField] private bool _isCapturedByEnemy;

    private bool _isConnected;
    private readonly int _maxPoints = 100;
    private Coroutine _increase;
    private Coroutine _decrease;

    public UnityAction<int> PointsChanged;

    private void Start()
    {
        PointsChanged?.Invoke(_points);
    }

    public void TryCapture()
    {
        _isConnected = true;

        if (_isNeutral || _isCapturedByEnemy)
        {
            _decrease = StartCoroutine(Decreasing());

            if(_points == 0)
            {
                StopCoroutine(_decrease);
                _increase = StartCoroutine(Increasing());
            }
        }
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

    private void TakeAwayPoint()
    {
        _points--;
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

    private IEnumerator Decreasing()
    {
        while (_points > 0 && _isConnected)
        {
            TakeAwayPoint();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
