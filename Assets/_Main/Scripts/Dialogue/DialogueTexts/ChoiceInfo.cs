using System.Collections.Generic;
using UnityEngine;

namespace CustomType
{
    [CreateAssetMenu(fileName = "ChoiceText", menuName = "Dialogues/Choice Text", order = 1)]
    public class ChoiceInfo : ScriptableObject
    {
        public string characterName;
        
        public Sprite characterPortrait;
        
        [TextArea(3,10)]
        public List<string> dialogues;

        public string choice1;
        public string choice2;
        
    }
}