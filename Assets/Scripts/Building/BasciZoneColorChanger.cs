using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasciZoneColorChanger : MonoBehaviour
{
    [SerializeField] private Building _building;
    [SerializeField] private ParticleSystem _zone;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private SpriteRenderer _glowRenderer;

    [System.Obsolete]
    private void Start()
    {
        ChangeColor(_building.CapturingSystem.CurrentTeam);
    }

    [System.Obsolete]
    private void OnEnable()
    {
        _building.CapturingSystem.TeamChanged += ChangeColor;

    }

    [System.Obsolete]
    private void OnDisable()
    {
        _building.CapturingSystem.TeamChanged -= ChangeColor;
    }

    [System.Obsolete]
    private void ChangeColor(Team team)
    {
        _zone.startColor = team.Color;
        _spriteRenderer.color = team.Color;
        Color color = team.Color;
        color.a = 0.25f;
        _glowRenderer.color = color;
    }
}
