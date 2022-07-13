using Obi;
using UnityEngine;

public class RopePickUpTrigger : MonoBehaviour
{
    [SerializeField] private ObiParticleAttachment _attachment;
    [SerializeField] private ObiRope _rope;
    [SerializeField] private Transform _ropeEnd;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            _ropeEnd.SetParent(player.RopePoint.transform);
            _ropeEnd.localPosition = Vector3.zero;

            //_rope.path.AddControlPoint(player.RopePoint.position, player.RopePoint.position, player.RopePoint.position,
            //    player.RopePoint.position, 0.1f, 0.1f, 1f, 0, Color.white, "point");

            //_attachment.target = player.transform;
        }
    }
}
