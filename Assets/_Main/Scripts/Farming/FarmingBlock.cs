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
        private bool _readyToHarvest = false;
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

            if (HighlightBlockFollowPointer.IsHoldingItem(ToolType.Sickle) && _readyToHarvest)
            {
                HarvestPlant();
            }
        }

        private void HarvestPlant()
        {
            Instantiate(_plantedSeed.harvestItem, transform.position, Quaternion.identity);
            DayNightCycle.Instance.OnDayPassed -= IncrementBlockState;
            Destroy(this.gameObject);
        }

        private void Start()
        {
            DayNightCycle.Instance.OnDayPassed += IncrementBlockState;
        }

        private void PlantSeed(PlantSeed seed)
        {
            _plantedSeed = seed;
            InventoryItem.CurrentlyAttached.itemCount -= 1;
            UpdateBlockState();
        }
        private void UpdateBlockState()
        {
            if (_currentBlockState == 0)
            {
                GetComponent<SpriteRenderer>().sprite = _plantedSeed.plantSprite;
                _isWatered = false;
            }
            if (_isWatered)
            {
                if (_currentBlockState > 0 && _currentBlockState < _plantedSeed.growingSprites.Count + 1)
                {
                    GetComponent<SpriteRenderer>().sprite = _plantedSeed.growingSprites[_currentBlockState - 1];
                    _isWatered = false;
                }
                else if (_currentBlockState >= _plantedSeed.growingSprites.Count + 1)
                {
                    GetComponent<SpriteRenderer>().sprite = _plantedSeed.readyToHarvestSprite;
                    _readyToHarvest = true;
                }

                // return;
            }
            //
            // if (_currentBlockState == 0)
            // {
            //     GetComponent<SpriteRenderer>().sprite = _plantedSeed.plantSprite;
            // }
            // else if (_currentBlockState > 0 && _currentBlockState < _plantedSeed.growingSprites.Count + 1)
            // {
            //     GetComponent<SpriteRenderer>().sprite = _plantedSeed.growingSprites[_currentBlockState - 1];
            // }
        }
        private void WaterPlant(bool watered)
        {
            _isWatered = watered;
            UpdateBlockState();
        }
        private void IncrementBlockState()
        {
            if (!_plantedSeed) return;
            UpdateBlockState();
            _currentBlockState++;
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