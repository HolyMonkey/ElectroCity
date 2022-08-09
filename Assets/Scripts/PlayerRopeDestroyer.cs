using System.Collections;
using UnityEngine;

public class PlayerRopeDestroyer : MonoBehaviour
{
    private Coroutine _coroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) && player.RopeHandler.HasRope)
        {
           _coroutine = StartCoroutine(Interacting(player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player) && player.RopeHandler.HasRope)
        {
            StopCoroutine(_coroutine);
        }
    }

    private IEnumerator Interacting(Player player)
    {
        yield return new WaitForSeconds(1f);

        player.RopeHandler.CurrentRope.Disconnect();
        player.TearRope();

        yield return new WaitForSeconds(0.5f);

        player.EffectsHandler.EnableTearEffect();
    }
}
