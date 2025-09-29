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
            _playerMovementAction = inputAction.action;
            if (_playerMovementAction == null)
            {
                Debug.LogError("playerMovementAction is null");
            }
        }
    
        protected virtual void OnEnable()
        {
            _playerMovementAction.Enable();
            _playerMovementAction.performed += OnMove;
            _playerMovementAction.canceled += OnMove;
        }

        protected virtual void OnDisable()
        {
            _playerMovementAction.Disable();
            _playerMovementAction.performed -= OnMove;
            _playerMovementAction.canceled -= OnMove;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MovementInput = _playerMovementAction.ReadValue<Vector2>();
        }

    }
}
