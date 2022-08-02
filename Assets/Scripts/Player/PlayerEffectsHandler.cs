using UnityEngine;

public class PlayerEffectsHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _winEffect1;
    [SerializeField] private ParticleSystem _winEffect2;

    public void EnableWinEffects()
    {
        _winEffect1.Play();
        _winEffect2.Play();
    }

    public void EnableLoseEffects()
    {

    }
}
