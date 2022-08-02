using System.Collections;
using UnityEngine;

public class RotationLocker : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }
}
