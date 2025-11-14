using UnityEngine;
using ScriptLibrary;

namespace Scripts.Dialogue
{
    public class TriggerObjectDialogueAdder : OnInteractTrigger2D
    {
        [SerializeField] private RuntimeDialogueGraph dialogueGraph;
        [SerializeField] private GameObject[] enableAfterDialogue;
        protected override void OnInteract()
        {
            DialogueManager.Instance.EnableThisObject(this, dialogueGraph, enableAfterDialogue);
            gameObject.SetActive(false);
        }
    }
}
