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
        if(other.TryGetComponent(out Player player) && !player.RopeHandler.HasRope && !_isPickingUp)
        {
            StartCoroutine(Taking(_delay, player));
        }
    }

    private IEnumerator Taking(float delay, Player player)
    {
        _isPickingUp = true;

        yield return new WaitForSeconds(delay);

        _ropeSpawner.Spawn(player);
        _isPickingUp = false;
    }
}
