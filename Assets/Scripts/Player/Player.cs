using System.Collections;
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

    private void OnEnable()
    {
        _ropeHandler.EnemyRopeBreaked += TearRope;
    }

    private void Awake()
    {
        _initialPos = transform.position;
    }

    private void OnDisable()
    {
        _ropeHandler.EnemyRopeBreaked -= TearRope;
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

    public void TearRope()
    {
        StartCoroutine(TearingRope());
    }

    private IEnumerator TearingRope()
    {
        _mover.Disable();
        _animator.StartJumpingDown();

        yield return new WaitForSeconds(0.5f);

        SoundHandler.Instance.PlayBreakSound();
        _effectsHandler.EnableTearEffect();

        yield return new WaitForSeconds(0.5f);

        _mover.Enable();
    }
}
