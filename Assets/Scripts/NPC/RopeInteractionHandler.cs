using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RopeInteractionHandler : MonoBehaviour
{
    private RopeInteractionHolder _ropeInteractionHolder;

    public RopePickUpTrigger GetClosestRopePickUp(Vector3 position)
    {
        RopePickUpTrigger closestRopeAttach = null;

        if (TryFindRopeHolder() == false)
            return closestRopeAttach;

        float distance = float.MaxValue;

        foreach (var ropePickUp in _ropeInteractionHolder.PickUp)
        {
            if (ropePickUp.IsConnected == false)
                continue;

            var tempDistance = Vector3.Distance(ropePickUp.transform.position, position);

            if (tempDistance <= distance)
            {
                distance = tempDistance;

                closestRopeAttach = ropePickUp;
            }
        }

        return closestRopeAttach;
    }

    public SetRopeTrigger GetClosestSetRope(Vector3 position)
    {
        SetRopeTrigger closestRopeAttach = null;

        if (TryFindRopeHolder() == false)
            return closestRopeAttach;

        float distance = float.MaxValue;

        foreach (var RopeSet in _ropeInteractionHolder.SetRopes)
        {
            if (RopeSet.IsConnected)
                continue;

            var tempDistance = Vector3.Distance(RopeSet.transform.position, position);

            if (tempDistance <= distance)
            {
                distance = tempDistance;

                closestRopeAttach = RopeSet;
            }
        }

        return closestRopeAttach;
    }

    private bool TryFindRopeHolder()
    {
        if (_ropeInteractionHolder != null)
            return true;

        _ropeInteractionHolder = FindObjectOfType<RopeInteractionHolder>();

        return _ropeInteractionHolder != null;
    }
}
