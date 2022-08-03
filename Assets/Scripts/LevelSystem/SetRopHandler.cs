using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRopHandler : MonoBehaviour
{
    [SerializeField] private List<SetRopeTrigger> _setRopeTriggers;

    private void Awake()
    {
        foreach (var trigger in _setRopeTriggers)
        {
            trigger.Init(this);
        }
    }

    public void Pick(SetRopeTrigger setRopeTrigger)
    {
        foreach (var trigger in _setRopeTriggers)
        {
            if (trigger != setRopeTrigger && trigger.IsAttaching ==false)
                trigger.IsAttaching = true;
        }
    }

    public void UnPick()
    {
        foreach (var trigger in _setRopeTriggers)
        {
            trigger.IsAttaching = false;
        }
    }
}
