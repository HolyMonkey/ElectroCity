using Obi;
using UnityEngine;

public class RopeSpawner : MonoBehaviour
{
    [SerializeField] private ObiRope _ropePrefab;
    [SerializeField] private ObiSolver _solver;
    [SerializeField] private ObiParticleAttachment _start;
    [SerializeField] private ObiParticleAttachment _end;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Transform _startPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            Instantiate(_ropePrefab, _solver.transform);

            _end.target = _endPoint;
            _endPoint.SetParent(player.RopePoint.transform);
            _endPoint.localPosition = Vector3.zero;
            _start.target = _startPoint;
        }
    }
}
