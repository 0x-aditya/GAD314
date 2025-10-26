using UnityEngine;
using ScriptLibrary.Inputs;
public class CloseAndOpenInventory : KeyPressInput
{
    [SerializeField] private GameObject inventoryUI;

    public void CloseOrOpenInventory()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
    protected override void OnKeyDown()
    {
        CloseOrOpenInventory();
    }
}
