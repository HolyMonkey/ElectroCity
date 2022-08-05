using System.Collections;
using UnityEngine;

public class PlayerRopeDestroyer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _tearEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            StartCoroutine(Interacting(player));
        }
    }

    private IEnumerator Interacting(Player player)
    {
        player.RopeHandler.CurrentRope.Disconnect();
        player.TearRope();

        yield return new WaitForSeconds(0.5f);

        _tearEffect.Play();
    }
}
