using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RopeHandler _ropeHandler;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private PlayerEffectsHandler _effectsHandler;
    [SerializeField] private PlayerCameraHandler _cameraHandler;

    public RopeHandler RopeHandler => _ropeHandler;
    public PlayerMover Mover => _mover;
    public PlayerAnimator Animator => _animator;
    public PlayerEffectsHandler EffectsHandler => _effectsHandler;
    public PlayerCameraHandler CameraHandler => _cameraHandler;

    private Vector3 _initialPos;

    private void Awake()
    {
        _initialPos = transform.position;
    }


    private void Update()
    {
        if (transform.position.y < -10)
        {
            transform.position = _initialPos;
            _mover.Enable();
            _effectsHandler.StartTrail();
        }
    }
}
