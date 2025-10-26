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
        [SerializeField] private int dayLengthInMinutes = 10;
        [SerializeField] private int startHour = 6;
        
        private float _currentTime;
        private int _currentDay = 1;
        private void Start()
        {
            dayText.text = dayLengthInMinutes.ToString();
            timeText.text = $"{startHour:D2}:00";
        }
        private void Update()
        {
            _currentTime += Time.deltaTime;
            float totalDaySeconds = dayLengthInMinutes * 60f;
            float timeRatio = _currentTime / totalDaySeconds;
            float currentHour = startHour + timeRatio * 24f;
            int displayHour = Mathf.FloorToInt(currentHour) % 24;
            int displayMinute = Mathf.FloorToInt((currentHour - Mathf.Floor(currentHour)) * 60);
            timeText.text = $"{displayHour:D2}:{displayMinute:D2}";

            if (_currentTime >= totalDaySeconds)
            {
                _currentTime = 0f;
                _currentDay++;
                dayText.text = $"Day: {_currentDay}";
                OnDayPassed?.Invoke();
            }
        }
    }
}