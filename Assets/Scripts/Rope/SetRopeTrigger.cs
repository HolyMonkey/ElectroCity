using System.Collections;
using Obi;
using UnityEngine;

public class SetRopeTrigger : MonoBehaviour
{
    [SerializeField] private Transform _connectPoint;
    [SerializeField] private Building _building;
    [SerializeField] private float _delay;

    private bool _isSet;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out RopeHandler handler) && handler.HasRope && !_isSet)
        {
            StartCoroutine(Attaching(_delay, handler));
        }
    }

    private IEnumerator Attaching(float delay, RopeHandler handler)
    {
        yield return new WaitForSeconds(delay);

        handler.PlaceRope(_connectPoint);
        _building.TryCapture();
        _isSet = true;
    }
}
