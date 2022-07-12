using GameAnalyticsSDK;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class Analytics : Singleton<Analytics>
{
    private const string IDGenerationChars = "abcdefghijklmnopqrstuvwxyz0123456789";
    private const int ProfileIdLength = 10;

    private string _profileId;

    private void Awake()
    {
        GameAnalytics.Initialize();
    }

    private void Start()
    {
        TrySendRegDayEvent();
        SendGameStartCountEvent();
        SendDaysPlayedEvent();
    }

    private void TrySendRegDayEvent()
    {
        bool isLogged = CustomPlayerPrefs.GetBool(AnalyticPrefs.FirstTimeLogPref);

        if (isLogged == false)
        {
            CustomPlayerPrefs.SetBool(AnalyticPrefs.FirstTimeLogPref, true);

            DateTime date = DateTime.Today;
            string dateString = date.ToString("dd/MM/yyyy");

            PlayerPrefs.SetString(AnalyticPrefs.FirstPlayDatePref, dateString);

            YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
            userProfile.Apply(YandexAppMetricaAttribute.CustomString("reg_day").WithValue(dateString));
            ReportUserProfile(userProfile);
        }
    }

    private void SendGameStartCountEvent()
    {
        int playsCount = PlayerPrefs.GetInt(AnalyticPrefs.StartsCountPref, 0);
        playsCount++;
        PlayerPrefs.SetInt(AnalyticPrefs.StartsCountPref, playsCount);

        Dictionary<string, object> eventProps = new Dictionary<string, object>();
        eventProps.Add("count", playsCount);

        SendAnalyticsEvents("game_start", eventProps);

        YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
        userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("sessions_count").WithDelta(playsCount));
        ReportUserProfile(userProfile);
    }

    private void SendDaysPlayedEvent()
    {
        try
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            string firstPlayDateString = PlayerPrefs.GetString(AnalyticPrefs.FirstPlayDatePref, DateTime.Now.ToString("dd/MM/yyyy"));
            var date = DateTime.ParseExact(firstPlayDateString, "dd/MM/yyyy", provider);
            int daysInGame = DateTime.Today.Subtract(date).Days;

            YandexAppMetricaUserProfile userProfile = new YandexAppMetricaUserProfile();
            userProfile.Apply(YandexAppMetricaAttribute.CustomCounter("days_in_game").WithDelta(daysInGame));
            ReportUserProfile(userProfile);
        }
        catch (FormatException)
        {
            throw new ArgumentException($"1{PlayerPrefs.GetString(AnalyticPrefs.FirstPlayDatePref, "")}1 is not valid date!");
        }
    }
    
    private static void SendAnalyticsEvents(string eventName, Dictionary<string, object> eventProps)
    {
        AppMetrica.Instance.ReportEvent(eventName, eventProps);
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, eventName, eventProps);
    }
    
    private void ReportUserProfile(YandexAppMetricaUserProfile userProfile)
    {
        AppMetrica.Instance.SetUserProfileID(GetProfileId());
        AppMetrica.Instance.ReportUserProfile(userProfile);
    }

    private string GetProfileId()
    {
        if (PlayerPrefs.HasKey(AnalyticPrefs.ProfileIDPref))
        {
            _profileId = PlayerPrefs.GetString(AnalyticPrefs.ProfileIDPref, "");
        }
        else
        {
            _profileId = GenerateProfileId(ProfileIdLength);
            PlayerPrefs.SetString(AnalyticPrefs.ProfileIDPref, _profileId);
        }

        return _profileId;
    }

    private string GenerateProfileId(int length)
    {
        var random = new System.Random();

        return new string(Enumerable.Repeat(IDGenerationChars, length)
            .Select(letter => letter[random.Next(letter.Length)]).ToArray());
    }
}

public static class AnalyticPrefs
{
    private const string FirstTimeLog = "FirstTimeLog";
    private const string FirstPlayDate = "FirstPlayDate";
    private const string ProfileID = "ProfileId";
    private const string StartsCount = "StartsCount";

    public static string FirstTimeLogPref => FirstTimeLog;
    public static string FirstPlayDatePref => FirstPlayDate;
    public static string ProfileIDPref => ProfileID;
    public static string StartsCountPref => StartsCount;
}
