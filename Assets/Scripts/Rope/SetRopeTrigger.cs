using System.Collections;
using UnityEngine;

public class SetRopeTrigger : MonoBehaviour
{
    [SerializeField] private Transform _connectPoint;
    [SerializeField] private Building _building;
    [SerializeField] private float _delay;

    private bool _isRopePlaced;

    public bool IsCapturedByPlayer => _building.IsCapturedByPlayer;
    public bool IsConnected => _building.IsConnected;

    private void OnTriggerEnter(Collider other)
    {
        if(CanAttach(other, out RopeHandler handler))
        {
            StartCoroutine(Attaching(_delay, handler));
        }
    }

    private IEnumerator Attaching(float delay, RopeHandler handler)
    {
        yield return new WaitForSeconds(delay);

        handler.PlaceRope(_connectPoint);
        _building.TryCapture();
        _isRopePlaced = true;
    }

    private bool CanAttach(Collider other, out RopeHandler handler)
    {
        return other.TryGetComponent(out handler) && handler.HasRope && !_isRopePlaced && !_building.IsConnected;
    }
}
