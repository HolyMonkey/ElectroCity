using Obi;
using UnityEngine;

public class RopePickUpTrigger : MonoBehaviour
{
    [SerializeField] private ObiParticleAttachment _attachment;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _attachment.target = player.RopePoint.transform;
        }
    }
}
