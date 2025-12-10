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

        private void Start()
        {
            if (dialogueGraph == null)
            {
                Debug.LogError("DialogueGraph is not assigned in TriggerObjectDialogueAdder on " + gameObject.name);
            }
        }

        protected override void OnInteract()
        {
            DialogueManager.Instance.EnableThisObject(dialogueGraph, enableAfterDialogue);
            // foreach (var behaviour in enableAfterInteract)
            // {
            //     behaviour.enabled = true;
            // }
            this.enabled = false;
            
        }
    }
}
