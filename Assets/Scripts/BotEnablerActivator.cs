using UnityEngine;

public class BotEnablerActivator : MonoBehaviour
{
    private BotEnabler[] _enablers;

    private void Awake()
    {
        _enablers = FindObjectsOfType<BotEnabler>();
    }

    public void Enable()
    {
        foreach (BotEnabler enabler in _enablers)
        {
            enabler.Activate();
        }
    }
}
