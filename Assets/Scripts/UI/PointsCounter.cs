using TMPro;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] private Building _building;

    private TMP_Text _value;
    private float _initialTextSize;

    private void Awake()
    {
        _value = GetComponent<TMP_Text>();
        _initialTextSize = _value.fontSize;
    }

    private void OnEnable()
    {
        _building.CapturingSystem.PointsChanged += OnPointsChanged;
    }

    private void OnDisable()
    {
        _building.CapturingSystem.PointsChanged -= OnPointsChanged;
    }

    private void OnPointsChanged(int point)
    {
        if(point >= _building.CapturingSystem.MaxPoints)
        {
            if(_value.fontSize == _initialTextSize)
            {
                _value.fontSize /= 1.6f;
            }

            _value.text = "MAX";
        }
        else
        {
            _value.fontSize = _initialTextSize;
            _value.text = point.ToString();
        }
    }
}
