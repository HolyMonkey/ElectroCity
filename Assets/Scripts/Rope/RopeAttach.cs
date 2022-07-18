using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class RopeAttach : MonoBehaviour
{
    [SerializeField] private Transform _ropeEnd;
    [SerializeField] private Rope _rope;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _ropeEnd.transform.SetParent(player.RopeHandler.RopePoint.transform);
            player.RopeHandler.TakeRope(_rope);
            StartCoroutine(Attaching());
        }
    }

    private IEnumerator Attaching()
    {
        while(_ropeEnd.transform.localPosition != Vector3.zero)
        {
            _ropeEnd.transform.localPosition = Vector3.Lerp(_ropeEnd.transform.localPosition, Vector3.zero, Time.deltaTime);

            yield return null;
        }
    }
}
