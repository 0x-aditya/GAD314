using System;
using System.Collections.Generic;
using CustomType;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using ScriptLibrary.Singletons;
using UnityEngine.UI;


namespace Scripts.Dialogue
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        [SerializeField] private TextMeshProUGUI characterNameUI;
        [SerializeField] private TextMeshProUGUI dialogueTextUI;
        [SerializeField] private Image characterPortraitUI;
        [SerializeField] private AudioSource _dialogueAudio; //added audio
        
        public RuntimeDialogueGraph runtimeGraph;
        
        private Dictionary<string, RuntimeDialogueNode> _nodeLookup = new();
        private RuntimeDialogueNode _currentNode;

        private RuntimeDialogueNode _nextNode;
        private DialogueInfo _currentDialogueInfo;

        private Canvas _canvas;
        private Button _button;
        
        private GameObject[] _enableAfterDialogue;
        [HideInInspector] public TriggerObjectDialogueAdder triggerObjectDialogueAdder;
        
        public void EnableThisObject(TriggerObjectDialogueAdder triggerObjectDialogue, RuntimeDialogueGraph runtimeDialogueGraph, GameObject[] enableAfterDialogue)
        {
            if (PostFXManager.Instance != null) PostFXManager.Instance.BlurFX(1f);

            triggerObjectDialogueAdder = triggerObjectDialogue;
            runtimeGraph = runtimeDialogueGraph;
            _enableAfterDialogue = enableAfterDialogue;

            _canvas = GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>();
            _canvas.enabled = true;
            _button = _canvas.GetComponentInChildren<Button>();
            _button.onClick.AddListener(ButtonFunction);
            
            foreach (var node in runtimeGraph.allNodes)
            {
                _nodeLookup[node.nodeID] = node;
            }
            
            if (!string.IsNullOrEmpty(runtimeGraph.entryNodeID))
            {
                if (!_nodeLookup.TryGetValue(runtimeGraph.entryNodeID, out _currentNode))
                {
                    EndDialogue();
                }
                ShowNode(runtimeGraph.entryNodeID);
            }
            else
            {
                EndDialogue();
            }
        }

        private void ResetThisObject()
        {
            triggerObjectDialogueAdder = null;
            runtimeGraph = null;
            _enableAfterDialogue = null;
            _nodeLookup.Clear();
            _nodeLookup = null;
            _currentNode = null;
            _nextNode = null;
            _counter = 0;
            _button = null;
            _canvas = null;
            _button?.onClick.RemoveAllListeners();
        }

        private int _counter = 0;
        private void ShowNode(string nodeID)
        {

            var c = _currentNode.dialogueInfo.dialogues.Count;

            if (c > _counter)
            {
                _dialogueAudio.Play(); //added audio
                characterNameUI.text = _currentNode.dialogueInfo.characterName;
                dialogueTextUI.text = _currentNode.dialogueInfo.dialogues[_counter];
                _counter++;
                characterPortraitUI.sprite = _currentNode.dialogueInfo.characterPortrait;
            }
            else
            {
                if (!_nodeLookup.TryGetValue(nodeID, out _nextNode))
                {
                    EndDialogue();
                    return;
                }

                _counter = 0;
                _currentNode = _nextNode;
                characterNameUI.text = _currentNode.dialogueInfo.characterName;
                dialogueTextUI.text = _currentNode.dialogueInfo.dialogues[_counter];
                characterPortraitUI.sprite = _currentNode.dialogueInfo.characterPortrait;
            }
        }

        private void EndDialogue()
        {
            if (PostFXManager.Instance != null) PostFXManager.Instance.BlurFX(0f);

            foreach (var behaviour in _enableAfterDialogue)
            {
                behaviour.SetActive(true);
            }
            _canvas.enabled = false;
            
            //ResetThisObject();
        }

        public void ButtonFunction()
        {
            if (!string.IsNullOrEmpty(_currentNode.nextNodeID))
            {
                ShowNode(_currentNode.nextNodeID);
            }
            else
            {
                EndDialogue();
            }
        }
    }
}