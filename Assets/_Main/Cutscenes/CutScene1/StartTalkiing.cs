using Scripts.Dialogue;
using UnityEngine;

public class StartTalkiing : MonoBehaviour
{
    [SerializeField] private RuntimeDialogueGraph graph;
    void OnEnable()
    {
        DialogueManager.Instance.EnableThisObject(null, graph, new GameObject[]{});
    }

}
