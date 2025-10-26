using UnityEngine;

namespace ScriptLibrary.GOInputs
{
    public abstract class InitializeInput
    {
        public static T InitializeInputMethod<T>(GameObject currentObject) where T : BaseInput
        {
            var a = currentObject.AddComponent<T>();
            return a;
        }
    }
}