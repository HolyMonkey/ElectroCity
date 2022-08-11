using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera _playerCamera;
	[SerializeField] private CinemachineVirtualCamera _topViewCamera;

    [SerializeField] private CinemachineBrain _brain;

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
        _brain.m_DefaultBlend.m_Time = 2f;
        _topViewCamera.Priority = -1;
    }

    public void ChangeBack()
    {
        _brain.m_DefaultBlend.m_Time = 3f;
        _topViewCamera.Priority = 10;
    }
}
