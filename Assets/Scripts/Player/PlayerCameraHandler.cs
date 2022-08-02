using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCameraHandler : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera _endCamera;

    public void ActivateFinishCamera()
    {
        _endCamera.Priority = 10;
    }
}
