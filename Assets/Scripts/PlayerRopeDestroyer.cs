using System.Collections;
using UnityEngine;

public class PlayerRopeDestroyer : MonoBehaviour
{
    [SerializeField] private UITimer _uITimer;

    private Coroutine _coroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player) && player.RopeHandler.HasRope)
        {
           _coroutine = StartCoroutine(Interacting(player));
            _uITimer.StartCount(1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player) )
        {
            if(player.RopeHandler.HasRope)
                StopCoroutine(_coroutine);

            _uITimer.StopCount();
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
