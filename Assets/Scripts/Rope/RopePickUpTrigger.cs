using System.Collections;
using UnityEngine;

public class RopePickUpTrigger : MonoBehaviour
{
    [SerializeField] private RopeSpawner _ropeSpawner;
    [SerializeField] private Building _building;
    [SerializeField] private float _delay;

    private bool _isPickUped;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player) && !player.HasRope && !_isPickUped)
        {
            StartCoroutine(Taking(_delay, player));
        }
    }

    private IEnumerator Taking(float delay, Player player)
    {
        yield return new WaitForSeconds(delay);

        _ropeSpawner.Spawn(player);
        _isPickUped = true;
    }
}
