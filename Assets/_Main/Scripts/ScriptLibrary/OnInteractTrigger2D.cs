using System;
using ScriptLibrary.Inputs;
using UnityEngine;

namespace ScriptLibrary
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class OnInteractTrigger2D : KeyPressInput
    {
        [SerializeField] protected GameObject interactionIcon;

        protected bool Interacted = false;
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (!enabled) return;
            if (other.gameObject.CompareTag("Player"))
            {
                Interacted = true;
                if (interactionIcon != null)
                {
                    interactionIcon.SetActive(true);
                }
            }
        }
        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Interacted = false;
                if (interactionIcon != null)
                {
                    interactionIcon.SetActive(false);
                }
            }
        }
        
        protected abstract void OnInteract();

        protected override void OnKeyDown()
        {
            print("onKeyDown");
            if (Interacted)
            {
                OnInteract();
            }
        }
    }
}