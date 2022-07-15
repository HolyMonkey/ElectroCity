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
        if(other.TryGetComponent(out Player player))
        {
            if (player.HasRope && !_isSet)
            {
                StartCoroutine(Waiting(_delay, player));
            }
        }
    }

    private IEnumerator Waiting(float delay, Player player)
    {
        yield return new WaitForSeconds(delay);

        player.SetRope(_connectPoint);
        _building.IncreasePoints();
        _isSet = true;
    }
}
