using TMPro;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] private Building _building;

    private TMP_Text _value;

    private void Awake()
    {
        _value = GetComponent<TMP_Text>();
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
            _value.text = "MAX";
        }
        else
        {
            _value.text = point.ToString();
        }
    }
}
