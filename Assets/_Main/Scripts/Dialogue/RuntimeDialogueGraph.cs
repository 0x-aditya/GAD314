using System;
using System.Collections.Generic;
using CustomType;
using UnityEngine;


public class RuntimeDialogueGraph : ScriptableObject
{
    public string entryNodeID;
    public List<RuntimeDialogueNode> allNodes = new();
}

[Serializable]
public class RuntimeDialogueNode
{
    public string nodeID;
    public DialogueInfo dialogueInfo;
    public string nextNodeID;
}