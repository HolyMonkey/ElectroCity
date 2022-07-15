using Obi;
using UnityEngine;

public class LengthCounter : MonoBehaviour
{
    private ObiRope _rope;

    private void Awake()
    {
        _rope = GetComponent<ObiRope>();    
    }

    private void Update()
    {
        print(_rope.CalculateLength());
    }
}
