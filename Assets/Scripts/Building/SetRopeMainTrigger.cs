using System.Collections.Generic;
using UnityEngine;

public class SetRopeMainTrigger : MonoBehaviour
{
    [SerializeField] private List<SetRopeTrigger> _triggers;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out RopeHandler handler))
        {
            if(TryFindClosestTrigger(handler, out SetRopeTrigger setRopeTrigger))
                setRopeTrigger.Attach(handler);
        }
    }

    private bool TryFindClosestTrigger(RopeHandler handler, out SetRopeTrigger closestTrigger)
    {
        float distance = Mathf.Infinity;
        closestTrigger = null;

        for (int i = 0; i < _triggers.Count; i++)
        {
            var tempDistance = Vector3.Distance(handler.transform.position, _triggers[i].transform.position);

            if (distance > tempDistance && _triggers[i].IsFree)
            {
                closestTrigger = _triggers[i];
                distance = tempDistance;
            }
        }

        return closestTrigger != null;
    }
}
