using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RopeInteractionHandler : MonoBehaviour
{
    private RopeInteractionHolder _ropeInteractionHolder;

    public bool TryGetClosestRopePickUp(TeamId teamId, Vector3 position, out RopePickUpTrigger closestRopeAttach)
    {
        closestRopeAttach = null;

        if (TryFindRopeHolder() == false)
            return false;

        float distance = float.MaxValue;

        foreach (var ropePickUp in _ropeInteractionHolder.PickUp)
        {
            if (ropePickUp.TeamId != teamId || ropePickUp.IsThereFreeRope == false)
                continue;

            var tempDistance = Vector3.Distance(ropePickUp.transform.position, position);

            if (tempDistance <= distance)
            {
                distance = tempDistance;

                closestRopeAttach = ropePickUp;
            }
        }

        return closestRopeAttach != null;
    }

    public bool TryGetClosestSetRope(TeamId teamId, Vector3 position, out SetRopeTrigger closestSetRope)
    {
        closestSetRope = null;

        if (TryFindRopeHolder() == false)
            return false;

        float distance = float.MaxValue;

        foreach (var RopeSet in _ropeInteractionHolder.SetRopes)
        {
            if (RopeSet.TeamId == teamId || RopeSet.IsTryingPlaceTwice(teamId))
                continue;

            var tempDistance = Vector3.Distance(RopeSet.transform.position, position);

            if (tempDistance <= distance)
            {
                distance = tempDistance;

                closestSetRope = RopeSet;
            }
        }

        return closestSetRope != null;
    }

    private bool TryFindRopeHolder()
    {
        if (_ropeInteractionHolder != null)
            return true;

        _ropeInteractionHolder = FindObjectOfType<RopeInteractionHolder>();

        return _ropeInteractionHolder != null;
    }
}
