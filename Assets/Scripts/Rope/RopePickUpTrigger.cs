using System.Collections;
using UnityEngine;

public class RopePickUpTrigger : MonoBehaviour
{
    [SerializeField] private SetRopeTrigger _setTrigger;
    [SerializeField] private Transform _ropeEnd;
    [SerializeField] private Building _building;
    [SerializeField] private float _delay;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            StartCoroutine(Waiting(_delay, player));
        }
    }

    private IEnumerator Waiting(float delay, Player player)
    {
        yield return new WaitForSeconds(delay);

        _ropeEnd.SetParent(player.RopePoint.transform);
        _ropeEnd.localPosition = Vector3.zero;

        if(_building != null)
        {
            _building.StopIncreasingPoints();
        }

        yield return new WaitForSeconds(1f);

        if(_setTrigger != null)
        {
            _setTrigger.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
