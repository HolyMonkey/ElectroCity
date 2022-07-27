using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawner : MonoBehaviour
{
    [SerializeField] private Rope _ropePrefab;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _ropePoint;

    private RopeHandler _cachedRopeHandler;
    private Rope _cachedRope;

    private void Start()
    {
        _ropePoint.transform.position = transform.position;
    }

    public void SetRopePoint(Transform point)
    {
        _ropePoint = point;
    }

    public Rope Spawn(RopeHandler handler)
    {
        var rope = Instantiate(_ropePrefab, _ropePoint);
        rope.EndPoint.transform.position = transform.position;
        _cachedRope = rope;
        _cachedRopeHandler = handler;
        ChangeColor(rope, handler);
        rope.SetTeamId(handler.Team);
        AttachRope();

        return rope;
    }

    public void AttachRope()
    {
        _cachedRope.transform.position = _startPoint.position;
        _cachedRope.StartPoint.SetParent(_startPoint);

        _cachedRopeHandler.TakeRope(_cachedRope);
    }

    private void ChangeColor(Rope rope, RopeHandler handler)
    {
        rope.Renderer.material.color = handler.Team.Color;
        rope.Plug.MeshRenderer.material.color = handler.Team.Color;
    }
}
