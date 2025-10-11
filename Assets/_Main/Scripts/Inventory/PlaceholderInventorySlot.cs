using System;
using UnityEngine;
using ScriptLibrary.Inputs;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlaceholderInventorySlot : MonoBehaviour
{
    [SerializeField] public FarmPlant plantObject;
    [SerializeField] private int _itemCount;
    
    public int itemCount
    {
        get => _itemCount;
        set
        {
            if (value <= 0)
            {
                GetComponentInChildren<TextMeshProUGUI>().text = "";
                GetComponent<Image>().sprite = plantObject ? plantObject.inventoryPlaceholderIcon : null;
                GetComponent<Image>().color = Color.white;
                return;
            }
            GetComponentInChildren<TextMeshProUGUI>().text = value.ToString();
            GetComponent<Image>().sprite = plantObject ? plantObject.inventoryItemIcon : null;
            GetComponent<Image>().color = Color.white;
            _itemCount = value;
        }
    }
    
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
        itemCount = _itemCount;
    }

    private void OnClick()
    {
        if (!plantObject)
        {
            PointerInventory.Instance.DestroyOldPointerObject();
            return;
        }
        
        if (_itemCount > 0)
            PointerInventory.Instance.inventoryItem = plantObject;
    }

}
