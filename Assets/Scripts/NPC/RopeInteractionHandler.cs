using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

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

    public bool TryGetClosestSetRope(NavMeshAgent navMeshAgent, TeamId teamId, Vector3 position, out SetRopeTrigger closestSetRope)
    {
        closestSetRope = null;

        if (TryFindRopeHolder() == false)
            return false;

        float distance = float.MaxValue;

        foreach (var ropeSetTrigger in _ropeInteractionHolder.SetRopes)
        {
            if (CanReach(navMeshAgent, ropeSetTrigger.transform.position) == false)
                continue;

            if (ropeSetTrigger.TeamId == teamId || ropeSetTrigger.IsTryingPlaceTwice(teamId))
                continue;

            var tempDistance = Vector3.Distance(ropeSetTrigger.transform.position, position);

            if (tempDistance <= distance)
            {
                distance = tempDistance;

                closestSetRope = ropeSetTrigger;
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

    private bool CanReach(NavMeshAgent navMeshAgent, Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();

        navMeshAgent.CalculatePath(targetPosition, path);
        float distance = 0f;

        for (int i = 1; i < path.corners.Length; i++)
        {
            distance += Vector3.Distance(path.corners[i - 1], path.corners[i]);
        }

        if (IsBotGonaFail())
            distance = 0;

        return distance < 15f;
    }


    private bool IsBotGonaFail()
    {
        float chanceToFail = Random.Range(0, 101);

        return chanceToFail < 50;
    }
}
