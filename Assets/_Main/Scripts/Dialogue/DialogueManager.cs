using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Scripts.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public RuntimeDialogueGraph runtimeGraph;
        
        
        private Dictionary<string, RuntimeDialogueNode> _nodeLookup = new();
        private RuntimeDialogueNode _currentNode;


        private void Start()
        {
            foreach (var node in runtimeGraph.allNodes)
            {
                _nodeLookup[node.nodeID] = node;
            }
            
            if (!string.IsNullOrEmpty(runtimeGraph.entryNodeID))
            {
                ShowNode(runtimeGraph.entryNodeID);
            }
            else
            {
                EndDialogue();
            }
        }

        private int _counter = 0;
        private void ShowNode(string nodeID)
        {
            if (!_nodeLookup.TryGetValue(nodeID, out var value))
            {
                EndDialogue();
                return;
            }

            _currentNode = value;
            
            print("Node: "+_counter++);
            print(_currentNode.dialogueInfo.characterName);
            foreach (var dialogue in _currentNode.dialogueInfo.dialogues)
            {
                print(dialogue);
            }
            print("Dialogue Complete");
            
            ShowNode(_currentNode.nextNodeID);
            
        }

        private void EndDialogue()
        {
            
        }

        private void Update()
        {
            //return;
            // if (Mouse.current.leftButton.wasPressedThisFrame)
            // {
            //     if (!string.IsNullOrEmpty(_currentNode.nextNodeID))
            //     {
            //         ShowNode(_currentNode.nextNodeID);
            //     }
            //     else
            //     {
            //         EndDialogue();
            //     }
            // }
        }
    }
}