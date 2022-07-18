using UnityEngine;
using Obi;
using static Obi.ObiRope;
using System.Collections;

public class Rope : MonoBehaviour
{
    [SerializeField] private ObiRope _obiRope;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;

    public ObiRope ObiRope => _obiRope;
    public Transform StartPoint => _startPoint;
    public Transform EndPoint => _endPoint;

    private void OnEnable()
    {
        _obiRope.OnRopeTorn += Disappear;
    }

    private void OnDisable()
    {
        _obiRope.OnRopeTorn -= Disappear;
    }

    private void Disappear(ObiRope obiRope, ObiRopeTornEventArgs tearInfo)
    {
        StartCoroutine(Disappearing());
    }

    private IEnumerator Disappearing()
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }
}
