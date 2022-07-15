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
        _solver.transform.position = _startPoint.transform.position;
        var rope = Instantiate(_ropePrefab, _solver.transform);

        rope.StartPoint.SetParent(_startPoint);
        rope.EndPoint.SetParent(player.RopePoint);
        
        StartCoroutine(Attaching(rope));

        player.TakeRope(rope);
    }

    private IEnumerator Attaching(Rope rope)
    {
        float runningTime = 0;
        float moveTime = 0.1f;

        while (rope.StartPoint.localPosition != Vector3.zero && rope.EndPoint.localPosition != Vector3.zero)
        {
            runningTime += Time.deltaTime;
            rope.StartPoint.localPosition = Vector3.Lerp(rope.StartPoint.localPosition, Vector3.zero, runningTime/moveTime);
            rope.EndPoint.localPosition = Vector3.Lerp(rope.EndPoint.localPosition, Vector3.zero, runningTime / moveTime);

            yield return null;
        }

    }
}
