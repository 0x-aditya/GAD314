using UnityEngine;
using UnityEngine.InputSystem;

namespace ScriptLibrary.GOInputs
{
    public abstract class Vector2Input : BaseInput
    {
        public Vector2 vectorInput;

        protected override void OnInput(InputAction.CallbackContext context)
        {
            vectorInput = playerMovementAction.ReadValue<Vector2>();
        }

    }
}