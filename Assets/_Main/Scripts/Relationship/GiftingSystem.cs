using System.Linq;
using ScriptLibrary;
using Scripts.Dialogue;
using Scripts.Inventory;
using Scripts.Items;
using UnityEditor;
using UnityEngine;

public class GiftingSystem : OnInteractTrigger2D
{
    [SerializeField] private BaseItem[] desiredItems;
    [SerializeField] private RuntimeDialogueGraph acceptDialogue;
    [SerializeField] private RuntimeDialogueGraph rejectDialogue;
    [SerializeField] private DisableUntilNextDay disableUntilNextDay;
    
    private RelationshipStatus _relationshipStatus;
    private void Start()
    {
        _relationshipStatus = GetComponent<RelationshipStatus>();
        if (!_relationshipStatus)
            Debug.LogError("Relationship status mono behaviour not set in " + gameObject.name);
        if (desiredItems.Length == 0)
            Debug.LogError("No desired items set in GiftingSystem on " + gameObject.name);
        if (acceptDialogue == null)
            Debug.LogError("Accept dialogue not set in GiftingSystem on " + gameObject.name);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanAcceptGift())
        {
            base.OnTriggerEnter2D(collision);
        }
    }
    private void FixedUpdate()
    {
        if (!interactionIcon) return;
        if (CanAcceptGift())
        {
            if (!interactionIcon.activeSelf)
            {
                interactionIcon.SetActive(true);
            }
        }
        else
        {
            if (interactionIcon.activeSelf)
            {
                interactionIcon.SetActive(false);
            }
        }
    }
    protected override void OnInteract()
    {
        if (!CanAcceptGift()) return;
        
        if (TryTakeGift())
        {
            AcceptGift();
            _relationshipStatus.IncreaseAffection(1);
        }
        else
        {
            RejectGift();
        }
    }
    private void AcceptGift()
    {
        Debug.Log("Gift Accepted");
        disableUntilNextDay.DisableObject();
        DialogueManager.Instance.EnableThisObject(acceptDialogue, null);
    }

    private void RejectGift()
    {
        Debug.Log("Gift Rejected");
    }
    
    private bool CanAcceptGift()
    {
        bool isSlotOccupied = HighlightInventory.InventorySlotObject.GetComponent<InventorySlot>().isOccupied;
        if (!isSlotOccupied) return false;
        
        bool isItemDesired = desiredItems.Contains(HighlightInventory.InventorySlotObject.GetComponentInChildren<InventoryItem>().itemData);
        if (!isItemDesired) return false;
        
        return true;
    }

    private bool TryTakeGift()
    {
        return InventoryManager.Instance.RemoveItem(HighlightInventory.InventorySlotObject.GetComponentInChildren<InventoryItem>(), 1);
    }
}
