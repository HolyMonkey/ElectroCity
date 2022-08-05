using System.Collections;
using UnityEngine;

public class PlayerEffectsHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _winEffect1;
    [SerializeField] private ParticleSystem _winEffect2;
    [SerializeField] private ParticleSystem _shockEffect;
    [SerializeField] private ParticleSystem _smokeEffect;
    [SerializeField] private ParticleSystem _trailEffect;
    [SerializeField] private ParticleSystem _tearEffect;

    public void EnableWinEffects()
    {
        _winEffect1.Play();
        _winEffect2.Play();
    }

    public void EnableLoseEffects()
    {
        _shockEffect.Play();
        StartCoroutine(Disabling(_shockEffect, 1.5f));
        StartCoroutine(WaitingForActivation(_smokeEffect, 3f));
    }

    public void StartTrail()
    {
        _trailEffect.Play();
    }

    public void StopTrail()
    {
        _trailEffect.Stop();
    }

    public void EnableTearEffect()
    {
        _tearEffect.Play();
    }

    private IEnumerator Disabling(ParticleSystem effect, float delay)
    {
        yield return new WaitForSeconds(delay);

        effect.Stop();
    }

    private IEnumerator WaitingForActivation(ParticleSystem effect, float delay)
    {
        yield return new WaitForSeconds(delay);

        effect.Play();
    }
}
