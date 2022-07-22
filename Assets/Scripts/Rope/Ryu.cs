using System.Collections;
using System.Collections.Generic;
using Obi;
using UnityEngine;

public class Ryu : MonoBehaviour
{
    [SerializeField] private Hadouken _hadouken;

    public void LaunchHadouken(Rope rope, Building buildingFrom)
    {
        var hadouken = Instantiate(_hadouken, rope.transform, true);
        hadouken.Throw(rope, buildingFrom);
    }
}
