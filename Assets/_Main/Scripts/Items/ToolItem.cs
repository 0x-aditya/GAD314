using UnityEngine;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "New Tool", menuName = "ScriptableObjects/Tool", order = 1)]
    public class ToolItem : BaseItem
    {
        public ToolType toolType;
    }
    
    public enum ToolType
    {
        Hoe,
        WateringCan,
        Axe,
        Pickaxe,
        Sickle
    }
}