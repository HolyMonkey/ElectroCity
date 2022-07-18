using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RopeHandler _ropeHandler;

    public RopeHandler RopeHandler => _ropeHandler;

    private void Update()
    {
        //if (_currentRope != null && _currentRope.ObiRope.CalculateLength() >= 15f )
        //{
        //    _currentRope.ObiRope.stretchingScale = 3f;
        //    _obiRigidbody.kinematicForParticles = false;
        //}

        //if (_currentRope == null)
        //    _obiRigidbody.kinematicForParticles = true;
    }
}
