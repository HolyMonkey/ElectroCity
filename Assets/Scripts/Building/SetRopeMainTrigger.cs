using System.Collections.Generic;
using UnityEngine;

public class SetRopeMainTrigger : MonoBehaviour
{
    [SerializeField] private List<SetRopeTrigger> _triggers;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out RopeHandler handler))
        {
            FindClosestTrigger(handler, other);
        }
    }

    private void FindClosestTrigger(RopeHandler handler, Collider other)
    {
        float distance = Mathf.Infinity;
        SetRopeTrigger closestTrigger = null;

        for (int i = 0; i < _triggers.Count; i++)
        {
            if (distance > Vector3.Distance(handler.transform.position, _triggers[i].transform.position))
            {
                distance = Vector3.Distance(handler.transform.position, _triggers[i].transform.position);
                closestTrigger = _triggers[i];
                print(closestTrigger.CanAttach(other, out handler));
            }
        }

        if (closestTrigger != null && closestTrigger.CanAttach(other, out handler))
        {
            closestTrigger.Attach(handler);
            _triggers.Remove(closestTrigger);
        }
    }
}
