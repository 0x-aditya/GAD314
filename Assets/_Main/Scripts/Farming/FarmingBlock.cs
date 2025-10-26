using Scripts.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using Scripts.Inventory;

namespace Scripts.Farming
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class FarmingBlock : MonoBehaviour, IPointerClickHandler
    {
        private PlantSeed _plantedSeed;
        private int _currentBlockState = 0;
        private bool _isWatered = false;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!CharacterMovementController.WithingRange(transform.position)) return;
            
            if (HighlightBlockFollowPointer.IsHoldingItem(ToolType.WateringCan))
            {
                WaterPlant(true);
            }

            if (IsHoldingPlantSeed())
            {
                PlantSeed(seed: InventoryItem.CurrentlyAttached.itemData as PlantSeed);
            }
        }
        private void Start()
        {
            DayNightCycle.Instance.OnDayPassed += IncrementBlockState;
        }

        private void PlantSeed(PlantSeed seed)
        {
            _plantedSeed = seed;
            UpdateBlockState();
        }
        private void UpdateBlockState()
        {
            
        }
        private void WaterPlant(bool watered)
        {
            _isWatered = watered;
        }
        private void IncrementBlockState()
        {
            _currentBlockState++;
            UpdateBlockState();
        }
        private bool IsAttachedToPointer()
        {
            return InventoryItem.CurrentlyAttached != null;
        }

        private bool IsHoldingPlantSeed()
        {
            if (!IsAttachedToPointer()) return false;
            return InventoryItem.CurrentlyAttached.itemData is PlantSeed;
        }
    }
}