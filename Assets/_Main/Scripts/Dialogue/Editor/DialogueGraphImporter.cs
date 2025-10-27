using UnityEngine;
using Unity.GraphToolkit.Editor;
using UnityEditor;
using System;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using System.Linq;
using Codice.Client.BaseCommands;
using CustomType;


[ScriptedImporter(1, DialogueGraph.AssetExtension)]
public class DialogueGraphImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        DialogueGraph editorGraph = GraphDatabase.LoadGraphForImporter<DialogueGraph>(ctx.assetPath);
        RuntimeDialogueGraph runtimeGraph = ScriptableObject.CreateInstance<RuntimeDialogueGraph>();
        var nodeIDMap = new Dictionary<INode,string>();
        
        foreach (var node in editorGraph.GetNodes())
        {
            nodeIDMap[node] = Guid.NewGuid().ToString();
        }

        var startNode = editorGraph.GetNodes().OfType<StartNode>().FirstOrDefault();
        if (startNode != null)
        {
            var entryPort = startNode.GetOutputPorts().FirstOrDefault()?.firstConnectedPort;
            if (entryPort != null)
            {
                runtimeGraph.entryNodeID = nodeIDMap[entryPort.GetNode()];
            }
        }

        foreach (var iNode in editorGraph.GetNodes())
        {
            if (iNode is StartNode || iNode is EndNode)
                continue;
            
            var runtimeNode = new RuntimeDialogueNode() {nodeID = nodeIDMap[iNode]};
            if (iNode is DialogueNode dialogueNode)
            {
                ProcessDialogueNode(dialogueNode, runtimeNode, nodeIDMap);
            }
            
            runtimeGraph.allNodes.Add(runtimeNode);
        }            
        ctx.AddObjectToAsset("RuntimeData", runtimeGraph);
        ctx.SetMainObject(runtimeGraph);
    }
    private void ProcessDialogueNode(DialogueNode node, RuntimeDialogueNode runtimeNode, Dictionary<INode,string> nodeIDMap)
    {
        runtimeNode.dialogueInfo = GetPortValue<DialogueInfo>(node.GetInputPortByName("Details"));
        
        var nextNodePort = node.GetOutputPortByName("out")?.firstConnectedPort;
        if (nextNodePort != null)
            runtimeNode.nextNodeID= nodeIDMap[nextNodePort.GetNode()];
    }
    private T GetPortValue<T>(IPort port)
    {
        if (port == null) return default;
        if (port.isConnected)
        {
            if (port.firstConnectedPort.GetNode() is IVariableNode variableNode)
            {
                variableNode.variable.TryGetDefaultValue(out T value);
                return value;
            }
        }
        port.TryGetValue(out T fallbackValue);
        return fallbackValue;
    }
}