using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera _playerCamera;
	[SerializeField] private CinemachineVirtualCamera _topViewCamera;
    //[SerializeField] private float _delay;

    //private void Start()
    //{
    //    StartCoroutine(Changing());
    //}

    //private IEnumerator Changing()
    //{
    //    yield return new WaitForSeconds(_delay);

    //    _topViewCamera.Priority = -1;
    //}

    public void Change()
    {
        _topViewCamera.Priority = -1;
    }
}
