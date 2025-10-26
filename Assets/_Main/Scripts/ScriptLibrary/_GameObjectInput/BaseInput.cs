using UnityEngine;
using UnityEngine.InputSystem;

namespace ScriptLibrary.GOInputs{
    public abstract class BaseInput : MonoBehaviour
    {
            [SerializeField] private InputActionReference inputAction;
            protected InputAction playerMovementAction => inputAction.action;

            protected virtual void Awake()
            {
                if (playerMovementAction == null)
                {
                    Debug.LogError("playerMovementAction is null");
                }
            }

            protected virtual void OnEnable()
            {
                playerMovementAction.Enable();
                playerMovementAction.performed += OnInput;
                playerMovementAction.canceled += OnInput;
            }

            protected virtual void OnDisable()
            {
                playerMovementAction.Disable();
                playerMovementAction.performed -= OnInput;
                playerMovementAction.canceled -= OnInput;
            }

            protected abstract void OnInput(InputAction.CallbackContext context);

    }
}
