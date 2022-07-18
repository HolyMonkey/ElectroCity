using System.Collections;
using UnityEngine;

public class RopePickUpTrigger : MonoBehaviour
{
    [SerializeField] private RopeSpawner _ropeSpawner;
    [SerializeField] private Building _building;
    [SerializeField] private float _delay;

    private bool _isPickingUp;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out RopeHandler handler) && !handler.HasRope && !_isPickingUp)
        {
            StartCoroutine(Taking(_delay, handler));
        }
    }

    private IEnumerator Taking(float delay, RopeHandler handler)
    {
        _isPickingUp = true;

        yield return new WaitForSeconds(delay);

        _ropeSpawner.Spawn(handler);
        _isPickingUp = false;
    }
}
