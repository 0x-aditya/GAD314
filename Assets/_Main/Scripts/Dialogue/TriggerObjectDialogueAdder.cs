using UnityEngine;

namespace Scripts.Dialogue
{
    public class TriggerObjectDialogueAdder : MonoBehaviour
    {
        [SerializeField] private RuntimeDialogueGraph dialogueGraph;
        [SerializeField] private KeyCode interactionKey = KeyCode.E;
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player") && Input.GetKeyDown(interactionKey))
            {
                DialogueManager.Instance.EnableThisObject(this, dialogueGraph);
            }
        }
    }
}