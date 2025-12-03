using System;
using UnityEngine;
using ScriptLibrary.Singletons;
using TMPro;

namespace Scripts.DayCycle
{
    public class DayNightCycle : Singleton<DayNightCycle>
    {
        public Action OnDayPassedContinuous;
        public Action OnDayPassedNonContinuous;
        [Header("Text UI References")]
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI dayText;
        
        [Header("Day Cycle Settings")]
        [SerializeField] private float dayLengthInMinutes = 10f; // Length of a full day in real-time minutes
        [SerializeField] private int startHour = 6; // Starting hour of the day (0-23)
        [SerializeField] private TimeFormat timeFormat = TimeFormat.TwelveHour; // Time format for display
        
        private float _currentTime;
        public int currentDay = 1; // Tracks the current day number
        private readonly int _actualStartHour = 0; // time starts at 0 for calculation purposes
        public float getDayNightTime => _currentTime / (dayLengthInMinutes * 60f); // to get time for day/night visuals

        private void Start()
        {
            // initialize time display
            timeText.text = $"{startHour:D2}:00";
            dayText.text = $"Day: {currentDay}";
            _currentTime += startHour * (dayLengthInMinutes * 60f) / 12f;
        }
        private void Update()
        {
            _currentTime += Time.deltaTime; // Increment time based on real-time seconds
            
            //Calculate current hour and minute
            float totalDaySeconds = dayLengthInMinutes * 60f;
            float timeRatio = _currentTime / totalDaySeconds;
            float currentHour = _actualStartHour + timeRatio * 24f;
            int displayHour = Mathf.FloorToInt(currentHour) % 24;
            int displayMinute = Mathf.FloorToInt((currentHour - Mathf.Floor(currentHour)) * 60);

            if (_currentTime >= totalDaySeconds) // Skip to next day
            {
                SkipToNextDay();
            }

            if (timeFormat == TimeFormat.TwelveHour) // Convert to 12-hour format
            {
                if (displayHour > 12)
                {
                    displayHour -= 12;
                }
                else if (displayHour == 0)
                {
                    displayHour = 12;
                }
            }

            timeText.text = $"{displayHour:D2}:{displayMinute:D2}"; // Update time display
        }
        
        public void SkipToNextDay()
        {
            _currentTime = 0f; // Reset time for new day
            currentDay++; // Increment day count
            dayText.text = $"Day: {currentDay}"; // Update day display
            OnDayPassedContinuous?.Invoke(); // Trigger any events for day change
            OnDayPassedNonContinuous?.Invoke(); // Trigger continuous day passed events
            OnDayPassedNonContinuous = null; // Clear subscribers after invocation
        }

        private enum TimeFormat
        {
            TwentyFourHour,
            TwelveHour
        }
        
        
    }
}