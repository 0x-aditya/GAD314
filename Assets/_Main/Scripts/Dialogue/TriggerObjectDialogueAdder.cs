using UnityEngine;

namespace Scripts.Dialogue
{
    public class TriggerObjectDialogueAdder : MonoBehaviour
    {
        [SerializeField] private RuntimeDialogueGraph dialogueGraph;
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
            {
                DialogueManager.Instance.EnableThisObject(this, dialogueGraph);
            }
        }
    }
}