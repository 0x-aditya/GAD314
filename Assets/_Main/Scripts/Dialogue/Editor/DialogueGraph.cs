using System;
using UnityEngine;
using Unity.GraphToolkit.Editor;
using UnityEditor;

[Serializable]
[Graph(AssetExtension)]
public class DialogueGraph : Graph
{
    public const string AssetExtension = "dialoguegraph";
    
    [MenuItem("Assets/Create/Dialogues/Dialogue Graph",false)]
    public static void CreateDialogueGraph()
    {
        GraphDatabase.PromptInProjectBrowserToCreateNewAsset<DialogueGraph>();    
    }
}
