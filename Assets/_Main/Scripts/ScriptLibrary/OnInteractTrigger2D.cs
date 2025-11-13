using System;
using ScriptLibrary.Inputs;
using UnityEngine;

namespace ScriptLibrary
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class OnInteractTrigger2D : KeyPressInput
    {
        [SerializeField] private GameObject interactionIcon;

        private bool _interacted = false;
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                _interacted = true;
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
                _interacted = false;
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
            if (_interacted)
            {
                OnInteract();
            }
        }
    }
}