using UnityEngine;

public class ColorChanger : MonoBehaviour
{
	[SerializeField] private Building _building;
	[SerializeField] private MeshRenderer _meshRenderer;

    private float _maxValue = 10f;
    
    private void OnEnable()
    {
        _building.CapturingSystem.PointsAdded += ChangeColor;
    }

    private void OnDisable()
    {
        _building.CapturingSystem.PointsAdded -= ChangeColor;
    }

    public void ChangeColor(Color color, float value, float maxValue)
    {
        //float addValue = value * _maxValue/maxValue;
        //_meshRenderer.material.SetColor("_ColorGradient", color);
        //_meshRenderer.material.SetFloat("_GradientSize", addValue);
        _meshRenderer.material.color = color;
    }
}
