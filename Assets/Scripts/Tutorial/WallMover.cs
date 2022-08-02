using DG.Tweening;
using UnityEngine;

public class WallMover : MonoBehaviour
{
    [SerializeField] private Building _building;

    private void OnEnable()
    {
        _building.CapturingSystem.TeamChanged += OnTeamChanged;
    }

    private void OnDisable()
    {
        _building.CapturingSystem.TeamChanged -= OnTeamChanged;
    }

    private void OnTeamChanged(Team team)
    {
        transform.DOMoveY(-5f, 1f);
    }
}
