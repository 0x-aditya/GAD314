using System;
using UnityEngine;
using ScriptLibrary.Singletons;
using TMPro;

namespace Scripts.Farming
{
    public class DayNightCycle : Singleton<DayNightCycle>
    {
        public Action OnDayPassed;
        
        [Header("Text UI References")]
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI dayText;
        
        [Header("Day Cycle Settings")]
        [SerializeField] private float dayLengthInMinutes = 10f;
        [SerializeField] private int startHour = 6;
        [SerializeField] private TimeFormat timeFormat = TimeFormat.TwelveHour;
        
        private float _currentTime;
        private int _currentDay = 1;
        private int _actualStartHour = 0;
        private void Start()
        {
            timeText.text = $"{startHour:D2}:00";
            dayText.text = $"Day: {_currentDay}";
            _currentTime += startHour * (dayLengthInMinutes * 60f) / 12f;
        }
        private void Update()
        {
            _currentTime += Time.deltaTime;
            float totalDaySeconds = dayLengthInMinutes * 60f;
            float timeRatio = _currentTime / totalDaySeconds;
            float currentHour = _actualStartHour + timeRatio * 24f;
            int displayHour = Mathf.FloorToInt(currentHour) % 24;
            int displayMinute = Mathf.FloorToInt((currentHour - Mathf.Floor(currentHour)) * 60);

            if (_currentTime >= totalDaySeconds)
            {
                _currentTime = 0f;
                _currentDay++;
                dayText.text = $"Day: {_currentDay}";
                OnDayPassed?.Invoke();
            }

            if (timeFormat == TimeFormat.TwelveHour)
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

            timeText.text = $"{displayHour:D2}:{displayMinute:D2}";
        }

        private enum TimeFormat
        {
            TwentyFourHour,
            TwelveHour
        }
        
        
    }
}