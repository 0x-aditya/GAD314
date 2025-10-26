using UnityEngine;

namespace ScriptLibrary.Singletons
{
    public abstract class PersistantSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            if (Instance == this)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
