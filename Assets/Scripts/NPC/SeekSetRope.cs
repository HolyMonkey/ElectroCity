using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class SeekSetRope : ActionTask
{
    public BBParameter<SetRopeTrigger> ClosestSetTrigger;
    public BBParameter<RopeInteractionHandler> RopeInteractionHandler;
    public BBParameter<GameObject> Self;

    protected override void OnUpdate()
    {
        ClosestSetTrigger.value = RopeInteractionHandler.value.GetClosestSetRope(Self.value.transform.position);
        EndAction(ClosestSetTrigger.value != null);
    }
}
