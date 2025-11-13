using UnityEngine;
using ScriptLibrary;
using Scripts.DayCycle;

public class SkipDayOnInteract : OnInteractTrigger2D
{
    /// <summary>
    /// For Bed Interaction, skips to the next day.
    /// </summary>
    protected override void OnInteract()
    {
        DayNightCycle.Instance.SkipToNextDay();
    }
}
