using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class SeekRopePickUp : ActionTask
{
    public BBParameter<RopePickUpTrigger> ClosestRopePickUp;
    public BBParameter<RopeInteractionHandler> RopeInteractionHandler;
    public BBParameter<GameObject> Self;
    public BBParameter<Team> Team;

    protected override void OnUpdate()
    {
        bool foundedRopePickUP = RopeInteractionHandler.value.TryGetClosestRopePickUp(Team.value.TeamId, Self.value.transform.position, out RopePickUpTrigger ropePickUpTrigger);

        if (foundedRopePickUP)
            ClosestRopePickUp.value = ropePickUpTrigger;
        else
            ClosestRopePickUp.value = null;

        EndAction(foundedRopePickUP);
    }
}
