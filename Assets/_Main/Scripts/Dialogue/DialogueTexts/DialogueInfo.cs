using System.Collections.Generic;
using UnityEngine;

namespace CustomType
{
    [CreateAssetMenu(fileName = "DialogueText", menuName = "Dialogues/Dialogue Text", order = 0)]
    public class DialogueInfo : ScriptableObject
    {
        public string characterName;
        
        [TextArea(3, 10)]
        public List<string> dialogues;
        
        public Sprite characterPortrait;
    }
}