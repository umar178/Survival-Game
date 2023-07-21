using System;
using UnityEngine;
using TMPro;

public class DayNightCycle : MonoBehaviour
{
    public float maxExposure = 1;
    public float minExposure = 0.15f;

    [SerializeField]
    private float FirstQuarterHour = 9;

    [SerializeField]
    private float SecondQuarterHour = 3;

    [SerializeField]
    private float timeMultiplier;

    [SerializeField]
    private float startHour;

    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private Light sunLight;

    [SerializeField]
    private float sunriseHour;

    [SerializeField]
    private float sunsetHour;

    [SerializeField]
    private Color dayAmbientLight;

    [SerializeField]
    private Color nightAmbientLight;

    [SerializeField]
    private AnimationCurve lightChangeCurve;

    [SerializeField]
    private float maxSunLightIntensity;

    [SerializeField]
    private Light moonLight;

    [SerializeField]
    private float maxMoonLightIntensity;

    private DateTime currentTime;

    private TimeSpan sunriseTime;

    private TimeSpan sunsetTime;

    private TimeSpan FirstQuarterDay;
    private TimeSpan SecondQuarterDay;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
        FirstQuarterDay = TimeSpan.FromHours(FirstQuarterHour);
        SecondQuarterDay = TimeSpan.FromHours(SecondQuarterHour);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
        sunExposureController();
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);

        if (timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

    private void RotateSun()
    {
        float sunLightRotation;

        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);

        }
        else
        {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLightIntensity, 0, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;

        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }

    void sunExposureController()
    {
        float intensity = RenderSettings.skybox.GetFloat("_Exposure");

        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < FirstQuarterDay)
        {
            TimeSpan sunriseToMidDayDuration = CalculateTimeDifference(sunriseTime, FirstQuarterDay);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunrise.TotalMinutes / sunriseToMidDayDuration.TotalMinutes;

            intensity = Mathf.Lerp(minExposure, maxExposure, (float)percentage);
        }
        else if(currentTime.TimeOfDay > SecondQuarterDay && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToMidDayDuration = CalculateTimeDifference(SecondQuarterDay, sunsetTime);
            TimeSpan timeSinceMidDay = CalculateTimeDifference(SecondQuarterDay, currentTime.TimeOfDay);

            double percentage = timeSinceMidDay.TotalMinutes / sunriseToMidDayDuration.TotalMinutes;

            intensity = Mathf.Lerp(maxExposure, minExposure, (float)percentage);
        }

        RenderSettings.skybox.SetFloat("_Exposure", intensity);
    }

    public void TimeController(int Hour)
    {
        currentTime = currentTime.AddHours(Hour);
        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            RenderSettings.skybox.SetFloat("_Exposure", maxExposure);
        }
        else if(currentTime.TimeOfDay < sunriseTime && currentTime.TimeOfDay > sunsetTime)
        {
            RenderSettings.skybox.SetFloat("_Exposure", minExposure);
        }
    }
}
