using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class Rope : MonoBehaviour
{
    [SerializeField] private ObiRope _obiRope;
    [SerializeField] private Transform _endPoint;

    public ObiRope obiRope => _obiRope;
    public Transform endPoint => _endPoint;
}
