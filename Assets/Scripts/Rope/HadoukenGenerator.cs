using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadoukenGenerator : MonoBehaviour
{
    [SerializeField] private Ryu _ryu;
    [SerializeField] private Rope[] _ropes;


    private Building _building;

    private void Awake()
    {
        StartCoroutine(Throwing());
    }

    private IEnumerator Throwing()
    {
        while (true)
        {
            foreach (var rope in _ropes)
            {
                _ryu.LaunchHadouken(rope, _building);

            }

            yield return new WaitForSeconds(1f);
        }
    }
}
