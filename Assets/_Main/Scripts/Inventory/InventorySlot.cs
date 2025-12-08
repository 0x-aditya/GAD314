using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Inventory
{
    public class InventorySlot : MonoBehaviour, IPointerClickHandler
    {
        public bool isOccupied => transform.childCount > 0;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!InventoryItem.CurrentlyAttached) return;
            
            if (transform.childCount == 0)
            {
                InventoryItem.CurrentlyAttached.AttachToObject(transform);
            }
            else if (transform.childCount > 0)
            {
                var child = transform.GetChild(0);
                var childInventoryItem = child.gameObject.GetComponent<InventoryItem>();
                
                // if inventory slot taken, check if same type of plant and stackable
                // also checks if the stack is full
                if (childInventoryItem != null &&
                    InventoryItem.CurrentlyAttached.itemData == childInventoryItem.itemData &&
                    childInventoryItem.itemData.isStackable && 
                    InventoryItem.CurrentlyAttached.itemData.isStackable &&
                    childInventoryItem.itemCount < childInventoryItem.itemData.maxStackAmount)
                {
                    if (childInventoryItem.itemCount + InventoryItem.CurrentlyAttached.itemCount <= childInventoryItem.itemData.maxStackAmount)
                    {
                        // can fit all items in the stack
                        childInventoryItem.itemCount += InventoryItem.CurrentlyAttached.itemCount;
                        Destroy(InventoryItem.CurrentlyAttached.gameObject);
                        InventoryItem.CurrentlyAttached = null;
                    }
                    else
                    {
                        // can only fit some items in the stack
                        int spaceLeft = childInventoryItem.itemData.maxStackAmount - childInventoryItem.itemCount;
                        childInventoryItem.itemCount += spaceLeft;
                        InventoryItem.CurrentlyAttached.itemCount -= spaceLeft;
                    }
                }
            }
        }

    }
} 



// namespace Scripts.Inventory
// {
//     public class InventorySlot : MonoBehaviour, IPointerClickHandler
//     {
//         public bool isOccupied => transform.childCount > 0;
//
//         public void OnPointerClick(PointerEventData eventData)
//         {
//             if (!InventoryItem.CurrentlyAttached) return;
//
//             var draggedItem = InventoryItem.CurrentlyAttached;
//
//             if (!isOccupied)
//             {
//                 // Place item in empty slot
//                 draggedItem.AttachToObject(transform);
//             }
//             else
//             {
//                 var childItem = transform.GetChild(0).GetComponent<InventoryItem>();
//
//                 if (childItem == null) return;
//
//                 // If same type and stackable
//                 if (draggedItem.itemData == childItem.itemData &&
//                     childItem.itemData.isStackable)
//                 {
//                     int total = childItem.itemCount + draggedItem.itemCount;
//                     int maxStack = childItem.itemData.maxStackAmount;
//
//                     if (total <= maxStack)
//                     {
//                         // Merge completely
//                         childItem.itemCount = total;
//                         Destroy(draggedItem.gameObject);
//                         InventoryItem.CurrentlyAttached = null;
//                     }
//                     else
//                     {
//                         // Merge partially
//                         int spaceLeft = maxStack - childItem.itemCount;
//                         childItem.itemCount += spaceLeft;
//                         draggedItem.itemCount -= spaceLeft;
//                     }
//                 }
//                 else
//                 {
//                     // Swap items
//                     var tempParent = childItem.transform.parent;
//                     childItem.AttachToPointer();
//                     draggedItem.AttachToObject(tempParent);
//                 }
//             }
//         }
//     }
// }
