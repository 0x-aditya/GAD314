using System.Linq;
using NUnit.Framework.Constraints;
using ScriptLibrary;
using Scripts.Dialogue;
using Scripts.Inventory;
using Scripts.Items;
using UnityEditor;
using UnityEngine;

public class GiftingSystem : OnInteractTrigger2D
{
    [SerializeField] private BaseItem[] desiredItems;
    [SerializeField] private BaseItem[] undesiredItems;
    [SerializeField] private RuntimeDialogueGraph acceptDialogue;
    [SerializeField] private RuntimeDialogueGraph rejectDialogue;
    [SerializeField] private DisableUntilNextDay disableUntilNextDay;
    
    private RelationshipStatus _relationshipStatus;
    private void Start()
    {
        if (!disableUntilNextDay)
        {
            disableUntilNextDay = GetComponent<DisableUntilNextDay>();
        }
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
        
        if (TryTakeGift() && _desired)
        {
            AcceptGift();
            _relationshipStatus.IncreaseAffection(1);
        }
        else if (TryTakeGift() && !_desired)
        {
            RejectGift();
            _relationshipStatus.DecreaseAffection(1);
        }
        else 
        {
            Debug.Log("Failed to take gift from inventory");
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
        Debug.Log("Gift Accepted");
        disableUntilNextDay.DisableObject();
        DialogueManager.Instance.EnableThisObject(rejectDialogue, null);
    }

    private bool _desired = false;
    private bool CanAcceptGift()
    {
        bool isSlotOccupied = HighlightInventory.InventorySlotObject.GetComponent<InventorySlot>().isOccupied;
        if (!isSlotOccupied) return false;
        
        bool isItemDesired = desiredItems.Contains(HighlightInventory.InventorySlotObject.GetComponentInChildren<InventoryItem>().itemData);
        bool isItemUndesired = undesiredItems.Contains(HighlightInventory.InventorySlotObject.GetComponentInChildren<InventoryItem>().itemData);

        if (isItemDesired)
        {
            _desired = true;
        }
        else if (isItemUndesired)
        {
            _desired = false;
        }
        else
        {
            return false;
        }
        return true;
    }

    private bool TryTakeGift()
    {
        return InventoryManager.Instance.RemoveItem(HighlightInventory.InventorySlotObject.GetComponentInChildren<InventoryItem>(), 1);
    }
}
