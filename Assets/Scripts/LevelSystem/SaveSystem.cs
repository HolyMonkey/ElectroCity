using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem
{
    private const int FirstLevel = 1;
    private const string LevelKey = "LevelKey";
    private const string LevelNumber = "levelNumber";
    private const string Level = "level";
    private const string LevelLoop = "levelLoop";

    public static void SaveLevelsProgression(int index)
    {
        PlayerPrefs.SetInt(LevelKey, index);
    }

    public static int LoadLevelsProgression()
    {
        if (PlayerPrefs.HasKey(LevelKey))
            return PlayerPrefs.GetInt(LevelKey);

        return FirstLevel;
    }

    public static void SaveLevelNumber(int index)
    {
        PlayerPrefs.SetInt(LevelNumber, index);
    }

    public static int LoadLevelNumber()
    {
        if (PlayerPrefs.HasKey(LevelNumber))
            return PlayerPrefs.GetInt(LevelNumber);

        return 1;
    }

    public static void ResetLevelNumber()
    {
        PlayerPrefs.SetInt(LevelNumber, 1);
    }

    public static void SaveLevel(int index)
    {
        PlayerPrefs.SetInt(Level, index);
    }

    public static int LoadLevel()
    {
        if (PlayerPrefs.HasKey(Level))
            return PlayerPrefs.GetInt(Level);

        return FirstLevel;
    }

    public static void SaveLevelLoop(int index)
    {
        PlayerPrefs.SetInt(LevelLoop, index);
    }

    public static int LoadLevelLoop()
    {
        if (PlayerPrefs.HasKey(LevelLoop))
            return PlayerPrefs.GetInt(LevelLoop);

        return 0;
    }
}
