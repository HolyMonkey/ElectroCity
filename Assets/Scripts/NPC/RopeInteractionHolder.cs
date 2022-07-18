using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeInteractionHolder : MonoBehaviour
{
    [SerializeField] private List<RopePickUpTrigger> _pickUps;
    [SerializeField] private List<SetRopeTrigger> _setRopes;

    public IReadOnlyList<RopePickUpTrigger> PickUp => _pickUps;
    public IReadOnlyList<SetRopeTrigger> SetRopes => _setRopes;
}
