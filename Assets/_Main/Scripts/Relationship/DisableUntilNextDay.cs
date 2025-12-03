using Scripts.DayCycle;
using UnityEngine;

public class DisableUntilNextDay : MonoBehaviour
{
    private GameObject targetObject;
    public void DisableObject(GameObject obj)
    {
        targetObject = obj;
        obj.SetActive(false);
        DayNightCycle.Instance.OnDayPassedContinuous += EnableObject;
    }

    private void EnableObject()
    {
        targetObject.SetActive(true);
    }
}
