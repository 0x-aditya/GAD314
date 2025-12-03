using System;
using System.Collections.Generic;
using UnityEngine;
using ScriptLibrary.Singletons;
using Scripts.Inventory;
using Scripts.Items;

/*
public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private List<InventorySlot> slots = new();

    public bool AddItem(InventoryItem item, BaseItem itemData)
    {
        if (item == null || itemData == null) return false;

        int remaining = item.itemCount;

        foreach (InventorySlot slot in slots)
        {
            if (!slot.isOccupied) continue;

            var child = slot.GetComponentInChildren<InventoryItem>();
            if (child == null) continue;
            if (child.itemData != itemData) continue;
            if (!child.itemData.isStackable || !itemData.isStackable) continue;
            if (child.itemCount >= child.itemData.maxStackAmount) continue;

            int space = child.itemData.maxStackAmount - child.itemCount;
            if (remaining <= space)
            {
                child.itemCount += remaining;
                remaining = 0;
                Destroy(item.gameObject);
                return true;
            }
            else
            {
                child.itemCount += space;
                remaining -= space;
            }
        }

        if (remaining <= 0) return true;

        bool usedOriginal = false;
        print("2");
        foreach (InventorySlot slot in slots)
        {
            if (remaining <= 0) break;
            if (slot.isOccupied) continue;

            int assign = remaining;
            if (itemData.isStackable)
            {
                assign = Mathf.Min(remaining, itemData.maxStackAmount);
            }

            if (!usedOriginal)
            {
                item.AttachToObject(slot.transform);
                item.itemCount = assign;
                usedOriginal = true;
            }
            else
            {
                var clone = Instantiate(item, slot.transform.localPosition, Quaternion.identity);
                clone.itemCount = assign;
                clone.AttachToObject(slot.transform);
            }

            remaining -= assign;
        }

        return remaining <= 0;
    }
    
    public bool RemoveItem(InventoryItem item, int amount)
    {
        if (item == null || amount <= 0) return false;

        if (item.itemCount > amount)
        {
            item.itemCount -= amount;
            return true;
        }
        else if (item.itemCount == amount)
        {
            Destroy(item.gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }
}
*/



public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private List<InventorySlot> slots = new();

    public bool AddItem(InventoryItem item, BaseItem itemData)
    {
        if (item == null || itemData == null) return false;

        int remaining = item.itemCount;

        // Try stacking in existing slots
        foreach (var slot in slots)
        {
            if (!slot.isOccupied) continue;

            var child = slot.GetComponentInChildren<InventoryItem>();
            if (child == null) continue;
            if (child.itemData != itemData) continue;
            if (!child.itemData.isStackable) continue;
            if (child.itemCount >= child.itemData.maxStackAmount) continue;

            int space = child.itemData.maxStackAmount - child.itemCount;
            if (remaining <= space)
            {
                child.itemCount += remaining;
                Destroy(item.gameObject);
                return true;
            }
            else
            {
                child.itemCount += space;
                remaining -= space;
            }
        }

        // Place in empty slots
        foreach (var slot in slots)
        {
            if (remaining <= 0) break;
            if (slot.isOccupied) continue;

            int assign = itemData.isStackable ? Mathf.Min(remaining, itemData.maxStackAmount) : 1;

            if (remaining == item.itemCount)
            {
                item.AttachToObject(slot.transform);
                item.itemCount = assign;
            }
            else
            {
                var clone = Instantiate(item, slot.transform);
                clone.itemCount = assign;
                clone.AttachToObject(slot.transform);
            }

            remaining -= assign;
        }

        return remaining <= 0;
    }

    public bool RemoveItem(InventoryItem item, int amount)
    {
        if (item == null || amount <= 0) return false;

        if (item.itemCount > amount)
        {
            item.itemCount -= amount;
            return true;
        }
        else if (item.itemCount == amount)
        {
            Destroy(item.gameObject);
            return true;
        }

        return false;
    }
}
