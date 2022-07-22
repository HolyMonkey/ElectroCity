using UnityEngine;
using UnityEngine.UI;

public class CircleHandler : MonoBehaviour
{
    [SerializeField] private Building _building;
    [SerializeField] private Image[] _circles;
    [SerializeField] private Image[] _circlesWithOutline;

    private readonly int _maxPossibleRopesCounter = 3;

    private void OnEnable()
    {
        _building.EnergyChecked += OnEnergyChecked;
        _building.CapturingSystem.TeamChanged += ChangeColor;
        _building.PickUpedRopesChanged += OnPickUpedRopesChanged;
    }

    private void OnDisable()
    {
        _building.EnergyChecked -= OnEnergyChecked;
        _building.CapturingSystem.TeamChanged -= ChangeColor;
        _building.PickUpedRopesChanged -= OnPickUpedRopesChanged;
    }

    private void OnEnergyChecked(int value)
    {
        if(value == _maxPossibleRopesCounter)
        {
            foreach(var rope in _circlesWithOutline)
            {
                rope.gameObject.SetActive(true);
            }
                return;
        }

        for(int i = 1; i < _circlesWithOutline.Length; i++)
        {
            if(i < value)
            {
                _circlesWithOutline[i].gameObject.SetActive(true);
            }
        }

        _circlesWithOutline[_circlesWithOutline.Length - 1 - value].gameObject.SetActive(false);
    }

    private void OnPickUpedRopesChanged()
    {
        if (_building.PickUpedRopes == 0)
        {
           foreach(var circle in _circles)
           {
                ChangeAlpha(circle, 1);
           }

            return;
        }

        for (int i = 1; i <= _building.MaxPickUpedRopes; i++)
        {
            if(_building.PickUpedRopes < i)
            {
                ChangeAlpha(_circles[_building.MaxPickUpedRopes - i], 1);
            }

            if (_building.PickUpedRopes == i)
            {
                ChangeAlpha(_circles[_building.MaxPickUpedRopes - i], 0);
            }
        }
    }

    private void ChangeColor(Team team)
    {
        foreach(var circle in _circles)
        {
            circle.color = team.Color;
        }
    }

    private void ChangeAlpha(Image image, int value)
    {
        Color color = image.color;
        color.a = value;
        image.color = color;
    }
}
