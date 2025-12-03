using Scripts.DayCycle;
using UnityEngine;

public class DisableUntilNextDay : MonoBehaviour
{
    private GameObject targetObject;
    public void DisableObject(GameObject obj)
    {
        targetObject = obj;
        obj.SetActive(false);
        DayNightCycle.Instance.OnDayPassed += EnableObject;
    }

    private void EnableObject()
    {
        targetObject.SetActive(true);
    }
}
