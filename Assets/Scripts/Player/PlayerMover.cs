using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private float _yRotation;
    private Rigidbody _rigidbody;
    private Vector3 _targetDirection;

    private const string MouseX = "Mouse X";

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();   
    }

    public void Move()
    {
        _targetDirection = transform.position + (_moveSpeed * Time.deltaTime * transform.forward);
        _rigidbody.MovePosition(_targetDirection);
        Rotate();
    }

    private void Rotate()
    {
        _yRotation += Input.GetAxis(MouseX) * _rotationSpeed;
        transform.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}
