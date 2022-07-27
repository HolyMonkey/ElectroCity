using UnityEngine;

public class ColorChanger : MonoBehaviour
{
	[SerializeField] private Building _building;
	[SerializeField] private MeshRenderer _meshRenderer;

    private float _maxValue = 10f;

    private void Start()
    {
        ChangeColor(_building.CapturingSystem.CurrentTeam.Color);    
    }

    private void OnEnable()
    {
        _building.CapturingSystem.PointsAdded += ChangeColor;
    }

    private void OnDisable()
    {
        _building.CapturingSystem.PointsAdded -= ChangeColor;
    }

    public void ChangeColor(Color color, float value = 0, float maxValue = 0)
    {
        //float addValue = value * _maxValue/maxValue;
        //_meshRenderer.material.SetColor("_ColorGradient", color);
        //_meshRenderer.material.SetFloat("_GradientSize", addValue);
        _meshRenderer.material.color = color;
    }
}
