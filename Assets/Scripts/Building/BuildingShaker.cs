using UnityEngine;

public class BuildingShaker : MonoBehaviour
{
    [SerializeField] private Building _building;
    [SerializeField] private Animator _animator;

    private const string StartShaking = "StartShaking";
    private const string StopShaking = "StopShaking";

    private void OnEnable() => _building.SettedRopesChanged += SetAnimationTrigger;

    private void OnDisable() => _building.SettedRopesChanged -= SetAnimationTrigger;

    private void SetAnimationTrigger()
    {
        if(_building.SettedRopes.Count > 0)
        {
            _animator.SetTrigger(StartShaking);
            return;
        }

        _animator.SetTrigger(StopShaking);
    }
}
