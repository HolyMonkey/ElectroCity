using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class RotationLocker : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
