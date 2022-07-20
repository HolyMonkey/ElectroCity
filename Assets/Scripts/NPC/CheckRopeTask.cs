using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class CheckRopeTask : ActionTask
{
    public BBParameter<RopeHandler> _ropeHandler;

    protected override void OnUpdate()
    {
        EndAction(_ropeHandler.value.HasRope);
    }

}
