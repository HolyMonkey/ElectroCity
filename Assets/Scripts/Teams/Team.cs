using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    [SerializeField] private Color32 _color;
    [SerializeField] private TeamId _teamId;

    private int _totalAmount;

    public bool IsLost { get; private set; }
    public float Points { get; private set; }
    public int Percent => Mathf.RoundToInt(Points / _totalAmount * 100);

    public TeamId TeamId => _teamId;
    public Color32 Color => _color;

    public void AddPoints(int amount)
    {
        ChangePoints(amount);
    }

    public void TakePoints(int amount, out int points)
    {
        points = amount;

        if (Points - amount <= 0)
        {
            IsLost = true;
            points = (int)Points;
        }
        ChangePoints(-points);
    }

    public void ChangePoints(int amount)
    {
        Points += amount;
        Points = Mathf.Clamp(Points, 0, 100);
    }

    public void SetTotalAmount(int totalAmount)
    {
        _totalAmount = totalAmount;
    }

    public void SetPoint(int amount)
    {
        Points = amount;
    }
}


