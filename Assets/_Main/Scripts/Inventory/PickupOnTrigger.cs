using System;
using Scripts.Inventory;
using Scripts.Items;
using UnityEngine;

public class PickupOnTrigger : MonoBehaviour
{
    [SerializeField] InventoryItem item;

    [SerializeField]
    private PlantSeed seed;
    
    private int harvestAmount => seed ? seed.yieldAmount : 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var inventoryItem = Instantiate(item);
            inventoryItem.itemCount = harvestAmount;
            var itemPickup = InventoryManager.Instance.AddItem(inventoryItem, inventoryItem.itemData);
            if (itemPickup != null)
            {
                PickupItem(gameObject);
            }
        }
    }
    private void PickupItem(GameObject obj)
    {
        Destroy(obj);
    }
}
