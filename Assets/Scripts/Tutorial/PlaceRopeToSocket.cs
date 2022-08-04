using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceRopeToSocket : MonoBehaviour
{
    [SerializeField] private RopePickUpTrigger _ropePickUpTrigger;
    [SerializeField] private RopeHandler _ropeHandler;
    [SerializeField] private SetRopeTrigger _setRopeTrigger;


    private void Awake()
    {
        StartCoroutine(Placing());
    }

    private IEnumerator Placing()
    {
        _ropePickUpTrigger.SetCurrentRopeHandler(_ropeHandler);
        _ropePickUpTrigger.TakeTutor(0, _ropeHandler);
        yield return new WaitForSeconds(0.01f);
        _setRopeTrigger.Attach(_ropeHandler);
    }
}
