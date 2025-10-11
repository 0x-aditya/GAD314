using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Main.Scripts.Farming
{
    [RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
    public class FarmLand : MonoBehaviour
    {
        [Header("Input Actions")]
        [SerializeField] private InputActionReference clickAction;
        
        [Header("Farm Land Settings")]
        [SerializeField] private LandState currentState = LandState.Empty;
        
        [SerializeField] private Sprite emptyLandSprite;
        
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

        private void OnEnable()
        {
            clickAction.action.performed += OnClick;
            clickAction.action.Enable();
        }

        private void OnDisable()
        {
            clickAction.action.performed -= OnClick;
            clickAction.action.Disable();
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            if (!context.control.IsPressed()) return;
            if (CurrentPlant == null && currentState != LandState.Empty) return;
            
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
            switch (currentState)
            {
                case LandState.Empty:
                    PlantSeed();
                    break;
                case LandState.Planted:
                    StartGrowing();
                    break;
                case LandState.Growing:
                    UpdateGrowth();
                    break;
                case LandState.ReadyToHarvest:
                    Harvest();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _camera = Camera.main;
        }

        private void PlantSeed()
        {
            if (currentState != LandState.Empty) return;
            
            currentState = LandState.Planted;
            UpdateSprite(plantedLandSprite);
        }

        private void StartGrowing()
        {
            if (currentState != LandState.Planted) return;
            
            currentState = LandState.Growing;
            UpdateSprite(growingLandSprite);
        }

        private void Harvest()
        {
            if (currentState != LandState.ReadyToHarvest) return;
            
            currentState = LandState.Empty;
            UpdateSprite(emptyLandSprite);
        }

        private void UpdateGrowth()
        {
            currentState = LandState.ReadyToHarvest;
            UpdateSprite(readyToHarvestLandSprite);
        }
        
        private void UpdateSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
        
        private bool _isGrowing = false;

        private void Update()
        {
            if (currentState == LandState.Empty && !_isGrowing) return;
            
            StartCoroutine(GrowthCycle());
            _isGrowing = true;

        }
        
        private IEnumerator GrowthCycle()
        {
            yield return new WaitForSeconds(3f);
            HandleLandClick();
            _isGrowing = false;
        }


        /*private void UpdateFarmState()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            switch (currentState)
            {
                case LandState.Empty:
                    UpdateSprite(emptyLandSprite);
                    break;
                case LandState.Planted:
                    UpdateSprite(plantedLandSprite);
                    break;
                case LandState.Growing:
                    UpdateSprite(growingLandSprite);
                    break;
                case LandState.ReadyToHarvest:
                    UpdateSprite(readyToHarvestLandSprite);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void OnValidate()
        {
            UpdateFarmState();
        }*/
    }
}