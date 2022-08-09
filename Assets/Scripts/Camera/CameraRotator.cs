using Cinemachine;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera _camera;
	[SerializeField] private float _speed;

    private void Update()
    {
        transform.Rotate(0, _speed * Time.deltaTime, 0);
    }
}
