using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Scriptable Objects/Levels list")]
public class LevelsList : ScriptableObject
{
    [SerializeField] private AssetReference[] _scenes;

    private const int BossLevelIndex = 5;
    private AssetReference _currentScene;

    public int SceneCount => _scenes.Length;

    private const string CurrentLevelIndex = "CurrentLevelIndex";

    public AssetReference GetScene(int index)
    {
        index -= 1;
        _currentScene = _scenes[index];

        SaveCurrentIndex(index);

        return _currentScene;
    }

    public AssetReference GetCurrentScene()
    {
        Debug.Log(PlayerPrefs.GetInt(CurrentLevelIndex));
        if (PlayerPrefs.HasKey(CurrentLevelIndex))
        {
            _currentScene = _scenes[PlayerPrefs.GetInt(CurrentLevelIndex)];
        }
        else
        {
            _currentScene = _scenes[0];
        }

        return _currentScene;
    }

    public AssetReference GetRandomScene(int counter)
    {
        int index = 0;

        if (_scenes.Length > 1)
        {
            do
            {
                index = Random.Range(0, _scenes.Length);
            } while (index == counter);
        }

        _currentScene = _scenes[index];

        SaveCurrentIndex(index);

        return _currentScene;
    }

    private void SaveCurrentIndex(int index)
    {
        PlayerPrefs.SetInt(CurrentLevelIndex, (index));
    }

    public static bool BossLevelFilter(int index)
    {
        return (index % BossLevelIndex) + 1 == BossLevelIndex;
    }
}
