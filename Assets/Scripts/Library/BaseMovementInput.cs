using UnityEngine;
using UnityEngine.InputSystem;

namespace Library
{
    public abstract class BaseMovementInput : MonoBehaviour
    {
        [SerializeField] private InputActionReference inputAction;
        
        protected Vector2 MovementInput;
        
        private InputAction _playerMovementAction;

        protected virtual void Awake()
        {
            
            if (_playerMovementAction == null)
            {
                Debug.LogError("playerMovementAction is null");
            }
        }
    
        protected virtual void OnEnable()
        {
            Debug.Log("Enabling player movement action");
            _playerMovementAction.Enable();
            _playerMovementAction.performed += OnMove;
            _playerMovementAction.canceled += OnMove;
        }

        protected virtual void OnDisable()
        {
            Debug.Log("Disabling player movement action");
            _playerMovementAction.Disable();
            _playerMovementAction.performed -= OnMove;
            _playerMovementAction.canceled -= OnMove;
        }

        protected virtual void OnDestroy()
        {
            Debug.Log("Destroying player movement action");
            if (_playerMovementAction?.enabled != true) return;
            
            _playerMovementAction.Disable();
            _playerMovementAction.performed -= OnMove;
            _playerMovementAction.canceled -= OnMove;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log("Reading movement input");
            MovementInput = _playerMovementAction.ReadValue<Vector2>();
        }

    }
}
