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
        if(other.TryGetComponent(out Player player) && player.HasRope && !_isSet)
        {
            StartCoroutine(Attaching(_delay, player));
        }
    }

    private IEnumerator Attaching(float delay, Player player)
    {
        yield return new WaitForSeconds(delay);

        player.PlaceRope(_connectPoint);
        _building.IncreasePoints();
        _isSet = true;
    }
}
