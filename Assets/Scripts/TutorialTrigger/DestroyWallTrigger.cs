using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWallTrigger : MonoBehaviour
{
    [SerializeField] private Building _building;
    [SerializeField] private GameObject _wall;
    [SerializeField] private ParticleSystem[] _particles;

    private void OnEnable()
    {
        _building.CapturingSystem.TeamChanged += ActivateDestraction;
    }

    private void OnDisable()
    {
        _building.CapturingSystem.TeamChanged -= ActivateDestraction;
    }

    private void ActivateDestraction(Team team)
    {
        if (team.TeamId == TeamId.First)
        {
            Destroy(_wall);

            foreach (var particle in _particles)
            {
                particle.Play();
            }
        }
    }
}
