using System.Collections;
using UnityEngine;

public class PlayerEffectsHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _winEffect1;
    [SerializeField] private ParticleSystem _winEffect2;
    [SerializeField] private ParticleSystem _shockEffect;
    [SerializeField] private ParticleSystem _smokeEffect;

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
