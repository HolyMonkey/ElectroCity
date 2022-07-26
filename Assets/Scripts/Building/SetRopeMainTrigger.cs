using System.Collections.Generic;
using UnityEngine;

public class SetRopeMainTrigger : MonoBehaviour
{
    [SerializeField] private List<SetRopeTrigger> _triggers;

    private bool _isFound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out RopeHandler handler))
        {
            int counter = 0;

            while (_isFound == false && counter < _triggers.Count)
            {
                FindClosestTrigger(handler, other, counter);
                counter++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out RopeHandler _))
        {
            _isFound = false;
        }
    }

    private void FindClosestTrigger(RopeHandler handler, Collider other, int value)
    {
        float minDistance = Mathf.Infinity;
        SetRopeTrigger closestTrigger = null;

        for (int i = 0; i < _triggers.Count - value; i++)
        {
            if (minDistance > Vector3.Distance(handler.transform.position, _triggers[i].transform.position))
            {
                minDistance = Vector3.Distance(handler.transform.position, _triggers[i].transform.position);
                closestTrigger = _triggers[i];
            }
        }

        if (closestTrigger != null && closestTrigger.CanAttach(other, out handler))
        {
            closestTrigger.Attach(handler);
            _isFound = true;
        }
    }
}
