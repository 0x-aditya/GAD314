using UnityEngine;

namespace Scripts.DayCycle
{
    public class NightObject : MonoBehaviour
    {
        private void Start()
        {
            DayNightVisuals.Instance.AddNightObject(this);
        }
        public virtual void ActivateNightObject(bool activate) { }
    }

}