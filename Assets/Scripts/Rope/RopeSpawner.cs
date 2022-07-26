using Obi;
using System.Collections;
using UnityEngine;

public class RopeSpawner : MonoBehaviour
{
    [SerializeField] private Rope _ropePrefab;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _ropePoint;

    private void Start()
    {
        _ropePoint.transform.position = transform.position;
    }

    public void SetRopePoint(Transform point)
    {
        _ropePoint = point;
    }

    public void Spawn(RopeHandler handler)
    {
        var rope = Instantiate(_ropePrefab, _ropePoint);

        ChangeColor(rope, handler);
        rope.SetTeamId(handler.Team);
        rope.transform.position = _startPoint.position;
        rope.StartPoint.SetParent(_startPoint);

        StartCoroutine(Delay(rope));

        handler.TakeRope(rope);
    }

    private void ChangeColor(Rope rope, RopeHandler handler)
    {
        rope.Renderer.material.color = handler.Team.Color;
        rope.Plug.MeshRenderer.material.color = handler.Team.Color;
    }

    private IEnumerator Delay(Rope rope)
    {
        yield return new WaitForSeconds(1f);

        rope.ObiRope.tearingEnabled = true;
    }
}
