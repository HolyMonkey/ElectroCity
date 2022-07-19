using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RopeInteractionHolder : MonoBehaviour
{
    private List<RopePickUpTrigger> _pickUps = new List<RopePickUpTrigger>();
    private List<SetRopeTrigger> _setRopes = new List<SetRopeTrigger>();

    public IReadOnlyList<RopePickUpTrigger> PickUp => _pickUps;
    public IReadOnlyList<SetRopeTrigger> SetRopes => _setRopes;

    private void Awake()
    {
        _pickUps = FindObjectsOfType<RopePickUpTrigger>().ToList();
        _setRopes = FindObjectsOfType<SetRopeTrigger>().ToList();
    }
}
