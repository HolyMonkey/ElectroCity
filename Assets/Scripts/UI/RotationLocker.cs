using System.Collections;
using UnityEngine;

public class RotationLocker : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
    }
}
