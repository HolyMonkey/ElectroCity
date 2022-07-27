using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEndGameHandler
{
    public event Action MoversEnded;

    public event Action Lose;
}
