using System;
using UnityEngine;
using ScriptLibrary.Singletons;
using Scripts.DayCycle;

// reference for stamina system
namespace Scripts.Stamina
{
    public class testscript : Singleton<testscript>
    {
        private void Start()
        {
            DayNightCycle.Instance.OnDayPassed += staminaSleep;
        }

        private void staminaSleep()
        {
            
        }

        public bool ReduceStamina(int amount)
        {
            return false;
        }
    }
}