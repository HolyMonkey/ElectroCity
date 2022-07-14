using System.Collections;
using UnityEngine;

public class SetRopeTrigger : MonoBehaviour
{
    [SerializeField] private Transform _ropeEnd;
    [SerializeField] private float _delay;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player _))
        {
            StartCoroutine(Waiting(_delay));
        }
    }

    private IEnumerator Waiting(float delay)
    {
        yield return new WaitForSeconds(delay);

        _ropeEnd.SetParent(transform);
        _ropeEnd.localPosition = Vector3.zero;
    }
}
