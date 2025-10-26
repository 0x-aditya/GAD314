using Scripts.Inventory;
using Scripts.Items;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HighlightBlockFollowPointer : MonoBehaviour
{
    private Camera _cam;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _cam = Camera.main;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _spriteRenderer.color = CharacterMovementController.WithingRange(MouseScreemToWorldPosition())
            ? Color.white
            : Color.red;

        RaycastHit2D rayHit = Physics2D.Raycast(MouseScreemToWorldPosition(), Vector3.forward);

        if (DisableHighlight(rayHit)) return; // if hit something that is not highlightable, disable highlight and exit

        if (IsHoldingItem(ToolType.Hoe))
        {
            HighLightIfTag(rayHit, "Grass"); // highlight if hit grass and holding hoe
        }

        else if (IsHoldingItem(ToolType.WateringCan))
        {
            HighLightIfTag(rayHit, "FarmingBlock");
        }
        else 
            _spriteRenderer.enabled = false;
    }

    private Vector3 MouseScreemToWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = _cam.ScreenToWorldPoint(mouseScreenPosition);
        mouseWorldPosition.z = 0; // moves camera to z = 0 so it doesn't go behind camera
        
        return mouseWorldPosition;
    }

    private bool DisableHighlight(RaycastHit2D rayHit)
    {
        // if no collider is hit, disable sprite renderer and exit
        if (rayHit.collider) 
            return false;
        
        _spriteRenderer.enabled = false;
        return true;
    }
    
    private void HighLightIfTag(RaycastHit2D rayHit, string blockTag)
    {
        // if collider has the specified tag, move highlight to that position and enable sprite renderer
        if (rayHit.collider.CompareTag(blockTag))
        {
            transform.position = rayHit.collider.transform.position;
            if (!_spriteRenderer.enabled)
                _spriteRenderer.enabled = true;
            
        }
        // otherwise, disable sprite renderer
        else
        {
            if (_spriteRenderer.enabled)
                _spriteRenderer.enabled = false;
        }
    }

    public static bool IsHoldingItem(ToolType toolType)
    {
        if (InventoryItem.CurrentlyAttached == null) // checks if you're holding an item
        {
            return false;
        }
        if (InventoryItem.CurrentlyAttached.itemData is ToolItem toolItem) // checks if the item is a tool
        {
            return toolItem.toolType == toolType; // returns true if the tool type matches
        }
        return false; // not holding a tool item
    }
}
