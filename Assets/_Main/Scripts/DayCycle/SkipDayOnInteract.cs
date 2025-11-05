using UnityEngine;
using ScriptLibrary;
using Scripts.DayCycle;

public class SkipDayOnInteract : OnInteractTrigger2D
{
    protected override void OnInteract()
    {
        DayNightCycle.Instance.SkipToNextDay();
        print("skipped Day");
    }
}
