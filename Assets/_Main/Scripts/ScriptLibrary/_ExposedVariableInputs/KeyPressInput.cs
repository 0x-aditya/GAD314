using UnityEngine.InputSystem;

namespace ScriptLibrary.Inputs
{
    public abstract class KeyPressInput : BaseInput
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            PlayerMovementAction.canceled += OnInput;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            PlayerMovementAction.canceled -= OnInput;
        }

        protected abstract void OnKeyPress();

        protected override void OnInput(InputAction.CallbackContext context)
        {
            OnKeyPress();
        }
    }
}