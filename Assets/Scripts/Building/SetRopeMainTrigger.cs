using System.Collections.Generic;
using UnityEngine;

public class SetRopeMainTrigger : MonoBehaviour
{
    [SerializeField] private List<SetRopeTrigger> _triggers;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out RopeHandler handler))
        {
            float minDistance = Mathf.Infinity;
            SetRopeTrigger closestTrigger = null;
                
            foreach (var trigger in _triggers)
            {
                if (minDistance > Vector3.Distance(handler.transform.position, trigger.transform.position))
                {
                    minDistance = Vector3.Distance(handler.transform.position, trigger.transform.position);
                    closestTrigger = trigger;
                }
            }

            if (closestTrigger != null && closestTrigger.CanAttach(other, out handler))
            {
                closestTrigger.Attach(handler);
            }
        }
    }
}
