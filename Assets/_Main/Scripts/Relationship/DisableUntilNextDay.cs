using Scripts.DayCycle;
using UnityEngine;

public class DisableUntilNextDay : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] targetScripts;
    [SerializeField] private bool disableOnStart = false;
    private void Start()
    {
        if (disableOnStart)
        {
            DisableObject();
        }
    }
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
