using UnityEngine;

public class SmokeActivator : MonoBehaviour
{
    [SerializeField] private Building _building;
    [SerializeField] private ParticleSystem _effect;

    [System.Obsolete]
    private void OnEnable()
    {
        _building.CapturingSystem.TeamChanged += OnTeamChanged;
    }

    [System.Obsolete]
    private void OnDisable()
    {
        _building.CapturingSystem.TeamChanged -= OnTeamChanged;

    }

    [System.Obsolete]
    private void OnTeamChanged(Team team)
    {
        _effect.startColor = team.Color;
        _effect.Play();
    }
}
