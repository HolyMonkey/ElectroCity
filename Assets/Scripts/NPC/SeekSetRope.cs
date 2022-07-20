using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class SeekSetRope : ActionTask
{
    public BBParameter<SetRopeTrigger> ClosestSetTrigger;
    public BBParameter<RopeInteractionHandler> RopeInteractionHandler;
    public BBParameter<GameObject> Self;
    public BBParameter<Team> Team;

    protected override void OnUpdate()
    {
        bool foundedRopePickUP = RopeInteractionHandler.value.TryGetClosestSetRope(Team.value.TeamId, Self.value.transform.position, out SetRopeTrigger setRopeTrigger);

        if (foundedRopePickUP)
            ClosestSetTrigger.value = setRopeTrigger;
        else
            ClosestSetTrigger.value = null;

        EndAction(foundedRopePickUP);
    }
}
