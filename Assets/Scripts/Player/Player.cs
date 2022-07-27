using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private RopeHandler _ropeHandler;

    public RopeHandler RopeHandler => _ropeHandler;
}
