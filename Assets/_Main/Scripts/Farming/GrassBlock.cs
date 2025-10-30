using UnityEngine;
using UnityEngine.EventSystems;
using Scripts.Items;

namespace Scripts.Farming
{
    public class GrassBlock : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject farmingBlockPrefab;
        private bool _isOccupied = false;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!CharacterMovementController.WithingRange(transform.position)) return;
            if (_isOccupied) return;
            
            if (HighlightBlockFollowPointer.IsHoldingItem(ToolType.Hoe))
            {
                MakeFarmBlock();
            }
        }

        private void MakeFarmBlock()
        {
            Instantiate(farmingBlockPrefab, transform.position, Quaternion.identity, transform);
            _isOccupied = true;
        }
    }
}