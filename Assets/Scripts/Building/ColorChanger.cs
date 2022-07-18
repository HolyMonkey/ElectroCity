using UnityEngine;

public class ColorChanger : MonoBehaviour
{
	[SerializeField] private Color _targetColor;
	[SerializeField] private Building _building;
	[SerializeField] private MeshRenderer _meshRenderer;

    private void OnEnable()
    {
        _building.ColorChanged += OnColorChanged;
    }

    private void OnDisable()
    {
        _building.ColorChanged = OnColorChanged;
    }

    private void OnColorChanged()
    {
		_meshRenderer.material.color = _targetColor;
    }
}
