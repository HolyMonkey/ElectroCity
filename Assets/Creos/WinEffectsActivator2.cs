using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEffectsActivator2 : MonoBehaviour
{
    [SerializeField] private WinnerDecider _decider;
	[SerializeField] private List<ParticleSystem> _effects;
    [SerializeField] private float _delay;

    private void OnEnable() => _decider.GameEnded += OnGameEnded; 

    private void OnDisable() => _decider.GameEnded -= OnGameEnded;

    private void OnGameEnded()
    {
        StartCoroutine(Starting());
    }

    private IEnumerator Starting()
    {
        yield return new WaitForSeconds(_delay);

        foreach (var effect in _effects)
        {
            effect.Play();
        }
    }
}
