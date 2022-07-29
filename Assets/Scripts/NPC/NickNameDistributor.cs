using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class NickNameDistributor : MonoBehaviour
{

    private List<Nick> _nicks;
    private List<string> _names = new List<string>
    { 
        "John",
        "Trooper",
        "Wolf",
        "Sparda",
        "Toxic",
        "Warrior",
        "Nagib",
        "Ganzi",
        "Noob"
    };

    private void Awake()
    {
        _nicks = FindObjectsOfType<Nick>().ToList();
    }

    private void Start()
    {
        foreach (var nick in _nicks)
        {
            int random = Random.Range(0, _names.Count);

            nick.Name.text = _names[random];
            _names.Remove(_names[random]);
        }
    }

}
