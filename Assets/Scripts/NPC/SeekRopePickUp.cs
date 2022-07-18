using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class SeekRopePickUp : ActionTask
{
    public BBParameter<RopePickUpTrigger> ClosestRopePickUp;
    public BBParameter<RopeInteractionHandler> RopeInteractionHandler;
    public BBParameter<GameObject> Self;

    protected override void OnUpdate()
    {
        ClosestRopePickUp.value = RopeInteractionHandler.value.GetClosestRopePickUp(Self.value.transform.position);
        EndAction(ClosestRopePickUp.value != null);
    }
}
