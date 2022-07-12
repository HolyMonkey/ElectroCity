using Obi;
using UnityEngine;

public class SetRopeTrigger : MonoBehaviour
{
    [SerializeField] private ObiParticleAttachment _attachment;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player _))
        {
            _attachment.target = transform;
        }
    }
}
