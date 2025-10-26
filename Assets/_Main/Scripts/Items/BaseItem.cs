using UnityEngine;

namespace Scripts.Items
{
    public class BaseItem : ScriptableObject
    {
        [Header("Item Base Info")]
        public string itemName;
        public Sprite itemIcon;
        public bool isStackable = true;
        public int maxStackAmount = 99;
    }
}