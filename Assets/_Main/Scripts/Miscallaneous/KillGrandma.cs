using System;
using Scripts.DayCycle;
using UnityEngine;

namespace _Main.Scripts.Miscallaneous
{
    public class KillGrandma : MonoBehaviour
    {
        [SerializeField] private GameObject grandma;
        [SerializeField] private GameObject inventoryEnabler;

        private void OnEnable()
        {
            inventoryEnabler.SetActive(true);
            DayNightCycle.Instance.OnDayPassedNonContinuous += KIllGrandma;
        }

        private void KIllGrandma()
        {
            grandma.SetActive(false);
        }
    }
}