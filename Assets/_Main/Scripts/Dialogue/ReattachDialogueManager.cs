using UnityEngine;

namespace Scripts.Dialogue
{
    public class ReattachDialogueManager : MonoBehaviour
    {
        public void Reattach()
        {
            DialogueManager dm = GetComponent<DialogueManager>();
            Destroy(dm);
            gameObject.AddComponent<DialogueManager>();
        }
    }
}