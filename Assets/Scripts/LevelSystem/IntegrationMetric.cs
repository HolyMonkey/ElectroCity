using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class IntegrationMetric
{
    private const string SessionCountName = "sessionCount";
    private const string _regDay = "regDay";

    private string _profileId;
    private const string ProfileId = "ProfileId";
    private const int ProfileIdLength = 10;

    public int SessionCount;

    public void OnGameStart()
    {
        Dictionary<string, object> count = new Dictionary<string, object>();
        count.Add("count", CountSession());

        //AppMetrica.Instance.ReportEvent("game_start", count);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "game_start", count);
    }

    public void OnLevelStart(int levelNumber, string levelName, int levelCount, int isLevelRandom, int levelLoop)
    {
        Dictionary<string, object> levelProperty = CreateLevelProperty(levelNumber, levelName, levelCount, isLevelRandom, levelLoop);

        AppMetrica.Instance.ReportEvent("level_start", levelProperty);
        AppMetrica.Instance.SendEventsBuffer();
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "level_start", levelProperty);
    }

    public void OnLevelComplete(int levelComplitioTime, int levelNumber, string levelName, int levelCount, int levelRandom, string result, int levelLoop, int progress)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object>
        {
            { "level_number", levelNumber },
            { "time", levelComplitioTime },
            { "level_name", levelName },
            { "level_count", levelCount },
            { "level_random", levelRandom },
            { "result", result },
            { "level_loop", levelLoop },
            { "progress", progress }
        };

        AppMetrica.Instance.ReportEvent("level_finish", userInfo);
        AppMetrica.Instance.SendEventsBuffer();
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level_complete", userInfo);
    }

    public void OnLevelFail(int levelFailTime, int levelIndex)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "level", levelIndex }, { "time_spent", levelFailTime }};

        //AppMetrica.Instance.ReportEvent("fail", userInfo);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "fail", userInfo);
    }

    public void OnRestartLevel(int levelIndex)
    {
        //var levelProperty = CreateLevelProperty(levelIndex);

        //AppMetrica.Instance.ReportEvent("restart", levelProperty);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "restart", levelProperty);

    }

    public void OnSoftCurrencySpend(string type, string name, int currencySpend)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "type", type }, { "name", name }, {"amount", currencySpend } };

        //AppMetrica.Instance.ReportEvent("soft_spent", userInfo);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "soft_spent", userInfo);

    }

    public void OnAbiltyUsed(string name)
    {
        Dictionary<string, object> userInfo = new Dictionary<string, object> { { "name", name } };

        //AppMetrica.Instance.ReportEvent("ability_used", userInfo);
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "ability_used", userInfo);
    }

    public void SetUserProperty()
    {

        //YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        //userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("session_count").WithDelta(SessionCount));
        //ReportUserProfile(userProfile);

        if (PlayerPrefs.HasKey(_regDay) == false)
        {
            RegDay();
        }
        else
        {
            int firstDay = PlayerPrefs.GetInt(_regDay);
            int daysInGame = DateTime.Now.Day - firstDay;

            DaysInGame(daysInGame);
        }
    }

    private void RegDay()
    {

        //YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        //userProfile.Apply(YandexAppMetricaAttribute.CustomString("reg_day").WithValue(DateTime.Now.ToString()));
        //ReportUserProfile(userProfile);

        PlayerPrefs.SetInt(_regDay, DateTime.Now.Day);
    }

    private void DaysInGame(int daysInGame)
    {

        //YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        //userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("days_in_game").WithDelta(daysInGame));
        //ReportUserProfile(userProfile);
    }

    //private void ReportUserProfile(YandexAppMetricaUserProfile userProfile)
    //{
    //    //AppMetrica.Instance.SetUserProfileID(GetProfileId());
    //    //AppMetrica.Instance.ReportUserProfile(userProfile);
    //}

    private string GetProfileId()
    {
        if (PlayerPrefs.HasKey(ProfileId))
        {
            _profileId = PlayerPrefs.GetString(ProfileId);
        }
        else
        {
            _profileId = GenerateProfileId(ProfileIdLength);
            PlayerPrefs.SetString(ProfileId, _profileId);
        }

        return _profileId;
    }

    private string GenerateProfileId(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        var random = new System.Random();

        return new string(Enumerable.Repeat(chars, length)
            .Select(letter => letter[random.Next(letter.Length)]).ToArray());
    }

    private Dictionary<string, object> CreateLevelProperty(int levelNumber, string levelName, int levelCount, int isLevelRandom, int levelLoop)
    {
        Dictionary<string, object> level = new Dictionary<string, object>();
        level.Add("level_number", levelNumber);
        level.Add("level_name", levelName);
        level.Add("level_count", levelCount);
        level.Add("level_random", isLevelRandom);
        level.Add("level_Loop", levelLoop);

        return level;
    }

    private int CountSession()
    {
        int count = 1;

        if (PlayerPrefs.HasKey(SessionCountName))
        {
            count = PlayerPrefs.GetInt(SessionCountName);
            count++;
        }

        PlayerPrefs.SetInt(SessionCountName, count);
        SessionCount = count;

        return count;
    }
}
