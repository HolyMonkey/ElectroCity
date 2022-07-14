using System.Collections;
using UnityEngine;

public class RopePickUpTrigger : MonoBehaviour
{
    [SerializeField] private Transform _ropeEnd;
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
    }
}
