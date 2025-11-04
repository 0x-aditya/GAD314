using System;
using UnityEngine;
using ScriptLibrary.Singletons;

namespace Scripts.Unlockables
{
    public class UnlockableManager : Singleton<UnlockableManager>
    {
        public bool sellBoxUnlocked = false;

        private void Start()
        {
            if (sellBoxUnlocked)
            {
                SellBoxUnlockable.UnlockImmediately();
            }
        }
    }
}