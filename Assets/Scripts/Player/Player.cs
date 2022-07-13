using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _ropePoint;

    public Transform RopePoint => _ropePoint;
}
