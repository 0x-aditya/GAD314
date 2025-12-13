using System;
using Scripts.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using Scripts.Inventory;
using Scripts.DayCycle;
using Unity.VisualScripting;

namespace Scripts.Farming
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class FarmingBlock : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] ParticleSystem wateringEffect;
        [SerializeField] ParticleSystem dirtEffect;

        private PlantSeed _plantedSeed;
        private int _currentBlockState = 0;
        private bool _isWatered = false;
        private bool _readyToHarvest = false;
        public void OnPointerDown(PointerEventData eventData)
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
            if (_readyToHarvest && eventData.button == PointerEventData.InputButton.Right)
            {
                HarvestPlant();
            }
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (CursorManager.Instance.leftCursorDown)
            {
                eventData.button = PointerEventData.InputButton.Left;
                OnPointerDown(eventData);
            }

            if (CursorManager.Instance.rightCursorDown)
            {
                eventData.button = PointerEventData.InputButton.Right;
                OnPointerDown(eventData);
            }
            if (!_readyToHarvest) return;
            
            if (CharacterMovementController.WithingRange(transform.position))
            {
                CursorManager.Instance.ChangeCursor(CursorManager.CursorType.Interact);
            }
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            CursorManager.Instance.ChangeCursor(CursorManager.CursorType.Default);
        }
        
        private void HarvestPlant()
        {
            CursorManager.Instance.ChangeCursor(CursorManager.CursorType.Default);
            PlayerStamina.Instance.ReduceStamina(1);
            ToolSounds.Instance.PlayHarvestAudio();
            Instantiate(_plantedSeed.harvestItem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        private void OnEnable()
        {
            dirtEffect.Play();
            DayNightCycle.Instance.OnDayPassedContinuous += IncrementBlockState;
            DayNightCycle.Instance.OnDayPassedContinuous += UpdateBlockState;
        }

        private void OnDisable()
        {
            DayNightCycle.Instance.OnDayPassedContinuous -= IncrementBlockState;
            DayNightCycle.Instance.OnDayPassedContinuous -= UpdateBlockState;
        }

        private void PlantSeed(PlantSeed seed)
        {
            if (_plantedSeed != null) return;
            
            dirtEffect.Play();
            _plantedSeed = seed;
            InventoryItem.CurrentlyAttached.itemCount -= 1;
            ToolSounds.Instance.PlayPlantSeedAudio();
            UpdateBlockState();
        }
        private void UpdateBlockState()
        {
            if (_plantedSeed == null) return;
            
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (_currentBlockState == 0)
            {
                spriteRenderer.sprite = _plantedSeed.plantSprite;
                _readyToHarvest = false;
                _isWatered = false;
                
            }
            if (_currentBlockState > 0 && (_currentBlockState <= _plantedSeed.growingSprites.Count))
            {
                spriteRenderer.sprite = _plantedSeed.growingSprites[_currentBlockState - 1];
                _isWatered = false;
            }
            if (_currentBlockState >= (_plantedSeed.growingSprites.Count + 1))
            {
                spriteRenderer.sprite = _plantedSeed.readyToHarvestSprite;
                _readyToHarvest = true;
            }
            ApplyTint();

            // return;
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
            PlayerStamina.Instance.ReduceStamina(1);
            ToolSounds.Instance.PlayWaterCanAudio();
            wateringEffect.Play();
            _isWatered = watered;
            ApplyTint();
        }
        private void ApplyTint()
        {
            if (_isWatered)
            {
                GetComponent<SpriteRenderer>().color = new Color(0.75f, 0.75f, 0.75f);
            }
            else
            {
                GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        private void IncrementBlockState()
        {
            if (!_plantedSeed) return;
            if (!_isWatered) return;
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