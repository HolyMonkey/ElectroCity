using System.Collections;
using Obi;
using UnityEngine;

public class SetRopeTrigger : MonoBehaviour
{
    [SerializeField] private RopePickUpTrigger _pickUpTrigger;
    [SerializeField] private Transform _connectPoint;
    [SerializeField] private Transform _ropeEnd;
    [SerializeField] private Building _building;
    [SerializeField] private float _delay;

    private ObiRope _rope;

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

        _ropeEnd.SetParent(_connectPoint);
        _ropeEnd.localPosition = Vector3.zero;
        _ropeEnd.SetParent(null);
        _building.IncreasePoints();

        yield return new WaitForSeconds(1f);

        //_pickUpTrigger.gameObject.SetActive(true);
        //gameObject.SetActive(false);
    }
}
