using Obi;
using UnityEngine;

public class RopePickUpTrigger : MonoBehaviour
{
    [SerializeField] private ObiParticleAttachment _attachment;
    [SerializeField] private ObiRope _rope;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _attachment.target = player.RopePoint.transform;

            //_rope.path.AddControlPoint(player.RopePoint.position, player.RopePoint.position, player.RopePoint.position,
            //    player.RopePoint.position, 0.1f, 0.1f, 1f, 0, Color.white, "point");

            //_attachment.target = player.transform;
        }
    }
}
