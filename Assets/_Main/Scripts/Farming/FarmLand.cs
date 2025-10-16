using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using ScriptLibrary.Inputs;

namespace _Main.Scripts.Farming
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class FarmLand : KeyPressInput
    {

        [Header("Farm Land Settings")]
        [SerializeField] private LandState currentState = LandState.Empty;
        
        [SerializeField] private Sprite emptyLandSprite;

        [Header("Current Plant Settings")]
        [SerializeField] private int currentDay;
        [SerializeField] private Seasons currentSeason;
        
        [NonSerialized] public FarmPlant CurrentPlant;
        private Sprite plantedLandSprite => CurrentPlant.plantSprite;
        private Sprite growingLandSprite => CurrentPlant.growingSprites[0];
        private Sprite readyToHarvestLandSprite => CurrentPlant.readyToHarvestSprite;
        
        private SpriteRenderer _spriteRenderer;
        private Camera _camera;
        private enum LandState
        {
            Empty,
            Planted,
            Growing,
            ReadyToHarvest
        }

        protected override void Awake()
        {
            base.Awake();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _camera = Camera.main;
        }

        protected override void OnKeyDown()
        {
            
            var mousePos = Mouse.current.position.ReadValue();
            var worldPos = _camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, _camera.nearClipPlane));
            var mousePos2D = new Vector2(worldPos.x, worldPos.y);

            var hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider && hit.collider.gameObject == gameObject)
            {
                HandleLandClick();
            }
        }
        private void HandleLandClick()
        {
            if (CurrentPlant == null && currentState == LandState.Empty)
            {
                if (PointerInventory.Instance.inventoryItem)
                {
                    CurrentPlant = PointerInventory.Instance.inventoryItem;
                    currentState = LandState.Planted;
                    _spriteRenderer.sprite = plantedLandSprite;
                    //StartCoroutine(GrowPlant());
                }
            }

        }
    }
}