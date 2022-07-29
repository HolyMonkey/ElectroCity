using TMPro;
using UnityEngine;

public class Nick : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;

    public TMP_Text Name => _name;
}
