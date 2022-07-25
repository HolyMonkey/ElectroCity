using UnityEngine;

public class Plug : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    public MeshRenderer MeshRenderer => _meshRenderer;
}
