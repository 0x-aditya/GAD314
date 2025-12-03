using ScriptLibrary.Singletons;
using UnityEngine.Rendering.Universal;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.DayCycle
{
    public class DayNightVisuals : Singleton<DayNightVisuals>
    {
        [SerializeField] Light2D sunLight;

        [Header("Thresholds")]
        [SerializeField, Range(0f, 1f)] float dawnStart = 0.14f;
        [SerializeField, Range(0f, 1f)] float dawnTime = 0.21f;
        [SerializeField, Range(0f, 1f)] float dayStart = 0.301f;
        [SerializeField, Range(0f, 1f)] float dayTime = 0.386f;
        [SerializeField, Range(0f, 1f)] float duskStart = 0.635f;
        [SerializeField, Range(0f, 1f)] float duskTime = 0.726f;
        [SerializeField, Range(0f, 1f)] float nightStart = 0.794f;
        [SerializeField, Range(0f, 1f)] float nightTime = 0.88f;

        [Header("Intensity")]
        [SerializeField] float sunDay = 1f;
        [SerializeField] float sunDawn = 1.0f;
        [SerializeField] float sunDusk = 1.1f;
        [SerializeField] float sunNight = 0.5f;

        [Header("Colors")]
        [SerializeField] private Color dayColor = Color.white;
        [SerializeField] private Color dawnColor = new Color(1f, 0.8f, 0.5f); // orange
        [SerializeField] private Color duskColor = new Color(1f, 0.3f, 0.2f); // red
        [SerializeField] private Color nightColor = new Color(0.1f, 0.1f, 0.3f); // blue

        private List<NightObject> nightObjects = new();
        private bool isNightActive;

        private void Update()
        {
            float t = DayNightCycle.Instance.getDayNightTime;

            sunLight.intensity = SetIntensity(t);
            sunLight.color = SetColor(t);
            SetNightObjects(t);
        }

        private float SetIntensity(float t)
        {
            if (t < dawnStart) return sunNight;
            if (t < dawnTime) return Mathf.Lerp(sunNight, sunDawn, Smooth((t - dawnStart) / (dawnTime - dawnStart)));
            if (t < dayStart) return sunDawn;
            if (t < dayTime) return Mathf.Lerp(sunDawn, sunDay, Smooth((t - dayStart) / (dayTime - dayStart)));
            if (t < duskStart) return sunDay;
            if (t < duskTime) return Mathf.Lerp(sunDay, sunDusk, Smooth((t - duskStart) / (duskTime - duskStart)));
            if (t < nightStart) return sunDusk;
            if (t < nightTime) return Mathf.Lerp(sunDusk, sunNight, Smooth((t - nightStart) / (nightTime - nightStart)));

            return sunNight;
        }

        private Color SetColor(float t)
        {
            if (t < dawnStart) return nightColor;
            if (t < dawnTime) return Color.Lerp(nightColor, dawnColor, Smooth((t - dawnStart) / (dawnTime - dawnStart)));
            if (t < dayStart) return dawnColor;
            if (t < dayTime) return Color.Lerp(dawnColor, dayColor, Smooth((t - dayStart) / (dayTime - dayStart)));
            if (t < duskStart) return dayColor;
            if (t < duskTime) return Color.Lerp(dayColor, duskColor, Smooth((t - duskStart) / (duskTime - duskStart)));
            if (t < nightStart) return duskColor;
            if (t < nightTime) return Color.Lerp(duskColor, nightColor, Smooth((t - nightStart) / (nightTime - nightStart)));

            return nightColor;
        }

        private void SetNightObjects(float t)
        {
            bool shouldBeNight = t < dawnStart || t >= nightStart;

            if (shouldBeNight == isNightActive) return;
            isNightActive = shouldBeNight;

            foreach (var obj in nightObjects)
                obj.ActivateNightObject(isNightActive);
        }

        public void AddNightObject(NightObject nightObject) => nightObjects.Add(nightObject);
        private float Smooth(float t) => t * t * (3f - 2f * t);
    }
}