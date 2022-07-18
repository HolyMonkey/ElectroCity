using Obi;
using System.Collections;
using UnityEngine;

public class RopeSpawner : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Rope _ropePrefab;
    [SerializeField] private ObiSolver _solver;
    [SerializeField] private Transform _startPoint;

    public void Spawn(RopeHandler handler)
    {
        _solver.transform.position = _startPoint.transform.position;
        var rope = Instantiate(_ropePrefab, _solver.transform);

        rope.StartPoint.SetParent(_startPoint);
        rope.EndPoint.SetParent(handler.RopePoint);
        
        StartCoroutine(Attaching(rope.StartPoint));
        StartCoroutine(Attaching(rope.EndPoint));
        StartCoroutine(Delay(rope));

        handler.TakeRope(rope);
    }

    private IEnumerator Attaching(Transform transform)
    {
        float elapsedTime = 0;
        float time = 0.5f;

        while (elapsedTime <= time)
        {
            elapsedTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, elapsedTime/time);

            yield return null;
        }
    }

    private IEnumerator Delay(Rope rope)
    {
        yield return new WaitForSeconds(1f);

        rope.ObiRope.tearingEnabled = true;
    }
}
