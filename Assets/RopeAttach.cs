using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeAttach : MonoBehaviour
{
    [SerializeField] private Transform _ropeEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _ropeEnd.transform.SetParent(player.RopePoint.transform);
            StartCoroutine(Movint());
        }
    }

    private IEnumerator Movint()
    {
        while(_ropeEnd.transform.localPosition != Vector3.zero)
        {
            _ropeEnd.transform.localPosition = Vector3.Lerp(_ropeEnd.transform.localPosition, Vector3.zero, Time.deltaTime);

            yield return null;
        }
    }
}
