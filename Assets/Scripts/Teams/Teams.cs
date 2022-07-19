using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teams : MonoBehaviour
{
    [SerializeField] private Team[] _teams;
    [SerializeField] private MultiColorSlider _multiColorSlider;

    private bool isSwaped;

    private void Awake()
    {
        _multiColorSlider.CreateBlank();
    }

    private void Update()
    {
        ReColor();
    }

    private void ReColor()
    {
        _multiColorSlider.Colorize(_teams);
    }
}

public enum TeamId
{
    First,
    Second,
    Third,
    Fourth,
    Netural
}
