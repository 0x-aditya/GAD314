using System;
using UnityEngine;
using ScriptLibrary.Singletons;
using UnityEngine.InputSystem;

public class PointerInventory : Singleton<PointerInventory>
{
    public GameObject inventoryItem;
    private Camera _mainCamera;
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (inventoryItem == null) return;
        
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        mousePosition.z = 10f;
        inventoryItem.transform.position = mousePosition;
    }
}
