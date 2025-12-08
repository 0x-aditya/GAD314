using Scripts.DayCycle;
using UnityEngine;

public class DisableUntilNextDay : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] targetScripts;
    public void DisableObject()
    {
        foreach (var script in targetScripts)
        {
            script.enabled = false;
        }
        DayNightCycle.Instance.OnDayPassedNonContinuous += EnableObject;
    }

    private void EnableObject()
    {
        foreach (var script in targetScripts)
        {
            script.enabled = true;
        }
    }
}
