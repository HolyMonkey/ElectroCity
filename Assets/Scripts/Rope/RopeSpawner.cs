using Obi;
using UnityEngine;

public class RopeSpawner : MonoBehaviour
{
    [SerializeField] private Rope _ropePrefab;
    [SerializeField] private ObiSolver _solver;
    [SerializeField] private Transform _startPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            var rope = Instantiate(_ropePrefab, _solver.transform);
            //_solver.transform.position = _startPoint.transform.position;
            _solver.transform.position = player.transform.position;

            rope.StartPoint.SetParent(_startPoint);
            rope.StartPoint.localPosition = Vector3.zero;

            rope.EndPoint.SetParent(player.RopePoint);
            rope.EndPoint.localPosition = Vector3.zero;
        }
    }
}
