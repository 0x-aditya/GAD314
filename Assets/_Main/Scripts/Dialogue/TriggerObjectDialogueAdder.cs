using System;
using UnityEngine;
using ScriptLibrary;

namespace Scripts.Dialogue
{
    public class TriggerObjectDialogueAdder : OnInteractTrigger2D
    {
        [SerializeField] private RuntimeDialogueGraph dialogueGraph;
        [SerializeField] private GameObject[] enableAfterDialogue;
        //[SerializeField] private MonoBehaviour[] enableAfterInteract;
        [SerializeField] private GameObject[] disableAfterDialogue;

        private void Start()
        {
            if (dialogueGraph == null)
            {
                Debug.LogError("DialogueGraph is not assigned in TriggerObjectDialogueAdder on " + gameObject.name);
            }
        }

        protected override void OnInteract()
        {
            if (disableAfterDialogue != null)
            {
                foreach (var obj in disableAfterDialogue)
                {
                    obj.SetActive(false);
                }
            }
            DialogueManager.Instance.EnableThisObject(dialogueGraph, enableAfterDialogue);
            // foreach (var behaviour in enableAfterInteract)
            // {
            //     behaviour.enabled = true;
            // }
            this.enabled = false;
            
        }
    }
}
