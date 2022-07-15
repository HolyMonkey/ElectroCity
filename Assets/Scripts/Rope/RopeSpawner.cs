using Obi;
using System.Collections;
using UnityEngine;

public class RopeSpawner : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Rope _ropePrefab;
    [SerializeField] private ObiSolver _solver;
    [SerializeField] private Transform _startPoint;

    public void Spawn(Player player)
    {
        _solver.transform.position = player.transform.position;
        var rope = Instantiate(_ropePrefab, _solver.transform);

        rope.StartPoint.SetParent(_startPoint);
        rope.StartPoint.localPosition = Vector3.zero;

        rope.EndPoint.SetParent(player.RopePoint);
        rope.EndPoint.localPosition = Vector3.zero;

        player.TakeRope(rope);
    }
}
