using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private LevelsHandler _levelsHandler;

    private IntegrationMetric _integrationMetric = new IntegrationMetric();

    private void Start()
    {
        //GameAnalytics.Initialize();
        _integrationMetric.OnGameStart();
        _levelsHandler.LoadNextLevel();
        //_integrationMetric.SetUserProperty();
    }
}
