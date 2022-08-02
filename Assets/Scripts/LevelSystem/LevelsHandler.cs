using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class LevelsHandler : MonoBehaviour
{
    [SerializeField] private LevelsList _levelList;
    [SerializeField] private bool _InitialLevel;

    private IntegrationMetric _integrationMetric = new IntegrationMetric();
    private float _timePassed;
    private bool _isLevelRandom;
    private int _levelNumber = 1;
    private bool _isCompleted;
    private int _levelLoop;
    private int _count = 1;

    public int Counter { get; private set; }

    private void Awake()
    {
        Counter = SaveSystem.LoadLevelsProgression();
        //_levelNumber = SaveSystem.LoadLevelNumber();
        _levelNumber = SaveSystem.LoadLevelsProgression();

        _count = SaveSystem.LoadLevel();
        _levelLoop = SaveSystem.LoadLevelLoop();
    }

    private void Start()
    {
        _timePassed = Time.time;

        if (_levelNumber > _levelList.SceneCount)
        {
            _isLevelRandom = true;
        }

        if (_InitialLevel == false)
        {
            string levelName = SceneManager.GetActiveScene().name;

            Counter++;

            SaveSystem.SaveLevelsProgression(Counter);

            //_integrationMetric.OnLevelStart(_levelNumber, levelName, Counter - 1, GetNumber(), _levelLoop);
            //print($"On level start. 1)levels_number: { _levelNumber}  2)level_name: {levelName} 3)level_count: {Counter - 1} 4)level_random {GetNumber()} 5)level_loop {_levelLoop}");
        }
    }

    private void OnApplicationQuit()
    {
        SaveSystem.ResetLevelNumber();
    }

    public void LoadNextLevel()
    {
        if (_levelNumber > _levelList.SceneCount)
        {
            _levelList.GetRandomScene(_levelNumber).LoadSceneAsync();
        }
        else
        {
            _levelList.GetScene(_levelNumber).LoadSceneAsync();
        }
    }

    public void RestartLevel()
    {
        //_integrationMetric.OnRestartLevel(Counter);

        var scene = _levelList.GetCurrentScene();

        Addressables.LoadSceneAsync(scene);
    }

    public void OnLevelCompleted()
    {
        _isCompleted = true;
        string levelName = SceneManager.GetActiveScene().name;
        int progress = 100;

        //_integrationMetric.OnLevelComplete(GetTime(), _levelNumber, levelName, Counter - 1, GetNumber(), GetLevelResult(), CheckLoop(), progress);
        //print($"On level completed.  1)level_number: {_levelNumber }, 2)level_name: {levelName}, 3)level_count: {Counter - 1}, 4)level_random: {GetNumber()}, 5)result: {GetLevelResult()} 5)level_loop {_levelLoop} 6)progress {progress}");

        _levelNumber++;
        _count++;

        SaveSystem.SaveLevelNumber(_levelNumber);
        SaveSystem.SaveLevel(_count);
    }

    public void OnLevelFailed()
    {
        _isCompleted = false;
        string levelName = SceneManager.GetActiveScene().name;
        int progress = 0;

        //_integrationMetric.OnLevelComplete(GetTime(), _levelNumber, levelName, Counter - 1, GetNumber(), GetLevelResult(), _levelLoop, progress);
        //print($"On level failed.  1)level_number: {_levelNumber }, 2)level_name: {levelName}, 3)level_count: {Counter -1}, 4)level_random: {GetNumber()}, 5)result: {GetLevelResult()} 5)level_loop {_levelLoop} 6)progress {progress}");

        //_integrationMetric.OnLevelFail(GetTime(), Counter);
    }

    private int GetTime()
    {
        return (int)(Time.time - _timePassed);
    }

    private int GetNumber()
    {
        return _isLevelRandom ? 1 : 0;
    }

    private string GetLevelResult()
    {
        return _isCompleted ? "win" : "lose";
    }

    private int CheckLoop()
    {
        if(_levelNumber % _levelList.SceneCount == 0)
        {
            _levelLoop++;
            SaveSystem.SaveLevelLoop(_levelLoop);
        }

        return _levelLoop;
    }
}
