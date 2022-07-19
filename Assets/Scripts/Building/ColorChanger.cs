using UnityEngine;

public class ColorChanger : MonoBehaviour
{
	[SerializeField] private Building _building;
	[SerializeField] private MeshRenderer _meshRenderer;
    
    private void OnEnable()
    {
        _building.ColorChanged += OnColorChanged;
    }

    private void OnDisable()
    {
        _building.ColorChanged -= OnColorChanged;
    }

    private void OnColorChanged(Color color)
    {
		_meshRenderer.material.color = color;
    }
}
