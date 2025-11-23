using System;
using UnityEngine;

public class HighlightInventory : MonoBehaviour
{
    [SerializeField] private GameObject[] inventorySlots;
    public static GameObject InventorySlotObject;

    private void Start()
    {
        InventorySlotObject = inventorySlots[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            transform.position = inventorySlots[9].transform.position;
            InventorySlotObject = inventorySlots[9];
        }
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                transform.position = inventorySlots[i].transform.position;
                InventorySlotObject = inventorySlots[i];
            }
        }
    }
}