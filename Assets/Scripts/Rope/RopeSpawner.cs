using Obi;
using System.Collections;
using UnityEngine;

public class RopeSpawner : MonoBehaviour
{
    [SerializeField] private Rope _ropePrefab;
    [SerializeField] private ObiSolver _solver;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _ropePoint;

    private void Awake()
    {
        _ropePoint.transform.position = transform.position;
    }

    public void Spawn(RopeHandler handler)
    {
        var rope = Instantiate(_ropePrefab, _ropePoint);

        ChangeColor(rope, handler);
        rope.SetTeamId(handler.Team);
        rope.transform.position = _startPoint.position;
        rope.StartPoint.SetParent(_startPoint);
        rope.EndPoint.SetParent(handler.RopePoint);

        rope.StartPoint.localPosition = Vector3.zero;
        rope.EndPoint.localPosition = Vector3.zero;

        StartCoroutine(Delay(rope));

        handler.TakeRope(rope);
    }

    private void ChangeColor(Rope rope, RopeHandler handler)
    {
        MeshRenderer renderer = rope.GetComponent<MeshRenderer>();
        renderer.material.color = handler.Team.Color;
    }

    private IEnumerator Delay(Rope rope)
    {
        yield return new WaitForSeconds(1f);

        rope.ObiRope.tearingEnabled = true;
    }
}
