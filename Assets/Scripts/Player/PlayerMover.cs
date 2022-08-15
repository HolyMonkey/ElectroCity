using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _speed;

    private Rigidbody _rigidBody;
    private PlayerAnimator _animator;
    private Player _player;
    private float _threshold = 0.01f;
    private const int LeftMouseButton = 0;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<PlayerAnimator>();
        _player = GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        Vector3 direcationForward = Camera.main.transform.forward * _joystick.Vertical;

        Vector3 directioRight = Camera.main.transform.right * _joystick.Horizontal;

        Vector3 direction = (direcationForward + directioRight).normalized;

        direction.y = 0;

        if (Input.GetMouseButton(LeftMouseButton) && direction.magnitude > _threshold)
        {
            Move(direction, _speed);
            Rotate(direction);

            if (_player.RopeHandler.HasRope)
            {
                _animator.StartRunWithRope();
                return;
            }

            _animator.StartRunning();
        }
        else
        {
            _animator.StartIdling();
        }
    }

    public void Move(Vector3 direction, float speed)
    {
        _rigidBody.MovePosition(transform.position + direction.normalized * speed * Time.deltaTime);
    }

    public void Disable()
    {
        enabled = false;
    }

    public void Enable()
    {
        enabled = true;
    }

    private void Rotate(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation.x = 0;
        lookRotation.z = 0;

        transform.rotation = lookRotation;
    }
}
