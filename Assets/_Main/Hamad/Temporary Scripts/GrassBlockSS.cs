using Scripts.Items;
using UnityEngine;
using UnityEngine.EventSystems;

public class GrassBlockSS : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject farmingBlockPrefab;
    [SerializeField] private PlayerStamina _playerS;
    private bool _isOccupied = false;

    public void OnPointerClick(PointerEventData eventData)
    {

        Debug.Log("I am clicked");
        if (!CharacterMovementController.WithingRange(transform.position)) return;
        if (_isOccupied) return;

        if (HighlightBlockFollowPointer.IsHoldingItem(ToolType.Hoe))
        {
            Debug.Log("This Should work");
            if (_playerS.OnStaminaUse())
            {
                MakeFarmBlock();
                _playerS.ReduceStamina(1);
            }
        }
    }

    private void MakeFarmBlock()
    {
        Instantiate(farmingBlockPrefab, transform.position, Quaternion.identity, transform);
        _isOccupied = true;
    }
}