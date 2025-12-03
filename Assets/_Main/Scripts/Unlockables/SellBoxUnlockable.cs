using System;
using Scripts.Farming;
using UnityEngine;
using Scripts.DayCycle;

namespace Scripts.Unlockables
{
    public class SellBoxUnlockable : MonoBehaviour
    {
        [SerializeField] GameObject sellBox;
        private void OnEnable()
        {
            DayNightCycle.Instance.OnDayPassedContinuous += EnableBox;
        }
        private void OnDisable()
        {
            DayNightCycle.Instance.OnDayPassedContinuous -= EnableBox;
        }

        private void EnableBox()
        {
            UnlockableManager.Instance.sellBoxUnlocked = true;
            sellBox.SetActive(true);
            gameObject.SetActive(false);
        }
        
        public static void UnlockImmediately()
        {
            UnlockableManager.Instance.sellBoxUnlocked = true;
            var sellBox = FindFirstObjectByType<SellBox>();
            sellBox.gameObject.SetActive(true);
        }
    }
}