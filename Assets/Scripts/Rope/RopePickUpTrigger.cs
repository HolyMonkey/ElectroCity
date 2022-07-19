using System.Collections;
using UnityEngine;

public class RopePickUpTrigger : MonoBehaviour
{
    [SerializeField] private RopeSpawner _ropeSpawner;
    [SerializeField] private Building _building;
    [SerializeField] private float _delay;
    
    private bool _isPickingUp;

    public bool IsTaken { get; private set; }
    public bool IsConnected => _building.IsConnected;

    private void OnTriggerEnter(Collider other)
    {
        if(CanTake(other, out RopeHandler handler))
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
        IsTaken = true;
    }

    private bool CanTake(Collider other, out RopeHandler handler)
    {
        return other.TryGetComponent(out handler) && !handler.HasRope && !_isPickingUp && handler.Team.TeamId == _building.TeamId;
    }
}
