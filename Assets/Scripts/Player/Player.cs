using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RopeHandler _ropeHandler;
    [SerializeField] private PlayerMover _mover;
    [SerializeField] private PlayerAnimator _animator;

    public RopeHandler RopeHandler => _ropeHandler;
    public PlayerMover Mover => _mover;
    public PlayerAnimator Animator => _animator;
}
