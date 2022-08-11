using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;

public class BotAnimationTask : ActionTask
{
    public BBParameter<BotAnimator> _botAnimator;

    protected override void OnUpdate()
    {
        _botAnimator.value.TriggerRun();

        EndAction(true);
    }
}
