using UnityEngine;

[RequireComponent(typeof(PlayerInput))]

public class PlayerInput : MonoBehaviour
{
    private PlayerMover _playerMover;
    private PlayerAnimator _animator; 

    private void Awake()
    {
        _playerMover = GetComponent<PlayerMover>();
        _animator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _playerMover.Move();
            _animator.SetRun();
        }
        else
        {
            _animator.SetIdle();
        }
    }
}
