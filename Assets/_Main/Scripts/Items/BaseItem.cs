using UnityEngine;

namespace Scripts.Items
{
    public class BaseItem : ScriptableObject
    {
        [Header("Item Base Info")]
        public string itemName;
        [TextArea(1,5)]
        public string itemDescription; 
        public Sprite itemIcon;
        public bool isStackable = true;
        public int maxStackAmount = 99;
        
    }
}