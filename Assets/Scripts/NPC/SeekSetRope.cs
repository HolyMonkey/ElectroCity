using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using UnityEngine.AI;

public class SeekSetRope : ActionTask
{
    public BBParameter<SetRopeTrigger> ClosestSetTrigger;
    public BBParameter<RopeInteractionHandler> RopeInteractionHandler;
    public BBParameter<GameObject> Self;
    public BBParameter<Team> Team;
    public BBParameter<NavMeshAgent> _navMeshAgent;

    protected override void OnUpdate()
    {
        bool foundedRopePickUP = RopeInteractionHandler.value.TryGetClosestSetRope(_navMeshAgent.value, Team.value.TeamId, Self.value.transform.position, out SetRopeTrigger setRopeTrigger);

        if (foundedRopePickUP)
            ClosestSetTrigger.value = setRopeTrigger;
        else
            ClosestSetTrigger.value = null;

        EndAction(foundedRopePickUP);
    }
}
