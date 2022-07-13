using Obi;
using UnityEngine;

public class LengthChanger : MonoBehaviour
{
    [SerializeField] private ObiRope _rope;
    [SerializeField] private ObiRopeCursor _cursor;
    [SerializeField] private float _speed;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _cursor.ChangeLength(_rope.restLength - _speed * Time.deltaTime);
        }

        if (Input.GetMouseButton(1))
        {
            _cursor.ChangeLength(_rope.restLength + _speed * Time.deltaTime);
        }
    }
}
