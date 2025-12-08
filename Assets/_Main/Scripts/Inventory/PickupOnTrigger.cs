using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.Inventory;
using Scripts.Items;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class PickupOnTrigger : MonoBehaviour
{
    [SerializeField] InventoryItem item;
    [SerializeField] private PlantSeed seed;
    
    private int harvestAmount => seed ? seed.yieldAmount : 2;

    private void OnEnable()
    {
        StartCoroutine(AnimatePopping());
    }

    private IEnumerator AnimatePopping()
    {
        float duration = 0.3f;
        float maxHeight = 0.5f;
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            float y = Mathf.Sin(t * Mathf.PI) * maxHeight;
            transform.position = startPos + Vector3.up * y;
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = startPos; // ensure exact reset
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var inventoryItem = Instantiate(item);
            inventoryItem.itemCount = harvestAmount;
            var itemPickup = InventoryManager.Instance.AddItem(inventoryItem, inventoryItem.itemData);
            if (itemPickup)
                PickupItem(gameObject);
        }
    }
    private void PickupItem(GameObject obj)
    {
        Destroy(obj);
    }
}
