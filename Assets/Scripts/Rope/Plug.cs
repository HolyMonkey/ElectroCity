using UnityEngine;

public class Plug : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    public MeshRenderer MeshRenderer => _meshRenderer;

    public void DESTRUCTION()
    {
        gameObject.SetActive(false);
    }

    public void SetHandRotation()
    {
        _meshRenderer.transform.localRotation = Quaternion.Euler(0f, 90f, 90f);
        _meshRenderer.transform.localPosition = Vector3.zero;
    }

    public void SetSocketRotatation()
    {
        _meshRenderer.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
        _meshRenderer.transform.localPosition = Vector3.up*0.2f;
    }
}
