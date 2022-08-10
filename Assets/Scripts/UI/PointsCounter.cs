using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] private Building _building;
    [SerializeField] private Image _image;

    private TMP_Text _value;
    private float _initialTextSize;

    private void Awake()
    {
        _value = GetComponent<TMP_Text>();
        _initialTextSize = _value.fontSize;
    }

    private void OnEnable()
    {
        _building.CapturingSystem.PointsChanged += Slidering;
    }

    private void OnDisable()
    {
        _building.CapturingSystem.PointsChanged -= Slidering;
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

    private void Slidering(int point)
    {
        float value = (float)point / _building.CapturingSystem.MaxPoints;
        _image.color = GetDarkerColor(_building.CapturingSystem.CurrentTeam.Color);
          
        _image.fillAmount = value;
    }

    private Color GetDarkerColor(Color color)
    {
        Color.RGBToHSV(color, out float h, out float s, out float v);

        v /= 1.1f;

        if (_building.CapturingSystem.CurrentTeam.TeamId == TeamId.Third)
            v /= 1.2f;

        Color colorRGB = Color.HSVToRGB(h, s, v);
        
        return colorRGB;
    }
}
