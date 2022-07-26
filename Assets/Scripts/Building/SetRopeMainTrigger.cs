using System.Collections.Generic;
using UnityEngine;

public class SetRopeMainTrigger : MonoBehaviour
{
    [SerializeField] private List<SetRopeTrigger> _triggers;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out RopeHandler handler))
        {
            FindClosestTrigger(handler);
        }
    }

    private void FindClosestTrigger(RopeHandler handler)
    {
        float distance = Mathf.Infinity;
        SetRopeTrigger closestTrigger = null;

        for (int i = 0; i < _triggers.Count; i++)
        {
            if (distance > Vector3.Distance(handler.transform.position, _triggers[i].transform.position))
            {
                distance = Vector3.Distance(handler.transform.position, _triggers[i].transform.position);

                if (_triggers[i].IsFree)
                    closestTrigger = _triggers[i];
            }
        }

        if (closestTrigger != null)
        {
            closestTrigger.Attach(handler);
            _triggers.Remove(closestTrigger);
        }
    }
}
