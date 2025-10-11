using System;
using System.Linq;
using ScriptLibrary.Singletons;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] public PlaceholderInventorySlot[] inventorySlots;

    private void Start()
    {
        inventorySlots = GetComponentsInChildren<PlaceholderInventorySlot>();
    }
    public void RemoveItem(FarmPlant plant, int amount = 1)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot == null) continue;
            if (slot.itemCount <= 0) continue;
            if (slot.itemCount < amount) continue;
            if (slot.plantObject != plant) continue;

            slot.itemCount -= amount;
            return;
        }
    }

    public void CheckForItem(FarmPlant plant, int amount = 1)
    {
        throw new NotImplementedException();
    }
}