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

        public static bool freezePlayer = false;
        
        public RuntimeDialogueGraph runtimeGraph;
        
        private Dictionary<string, RuntimeDialogueNode> _nodeLookup = new();
        private RuntimeDialogueNode _currentNode;

        private RuntimeDialogueNode _nextNode;
        private DialogueInfo _currentDialogueInfo;

        private Canvas _canvas;
        private Button _button;
        
        private string _lastDialogueText = "";
        
        private GameObject[] _enableAfterDialogue;
        [HideInInspector] public TriggerObjectDialogueAdder triggerObjectDialogueAdder;
        
        public void EnableThisObject(RuntimeDialogueGraph runtimeDialogueGraph, GameObject[] enableAfterDialogue)
        {
            if (PostFXManager.Instance != null) PostFXManager.Instance.BlurFX(1f);

            runtimeGraph = runtimeDialogueGraph;
            _enableAfterDialogue = enableAfterDialogue;

            _canvas = GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>();
            _canvas.enabled = true;
            _button = _canvas.GetComponentInChildren<Button>();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(ButtonFunction);

            _lastDialogueText = "";

            freezePlayer = true; //to freeze the player and prevent them from moving while talking
            
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

        private int _counter = 0;
        private void ShowNode(string nodeID)
        {
            // if current node has the same dialogue info as last time, skip to next dialogue
            
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
            if (_enableAfterDialogue != null)
            {
                foreach (var behaviour in _enableAfterDialogue)
                {
                    behaviour.SetActive(true);
                }
            }

            if (PostFXManager.Instance != null) PostFXManager.Instance.BlurFX(0f);

            _canvas.enabled = false;
            freezePlayer = false;
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
        
        private readonly float _dialogueCooldown = 0.2f;
        private bool _canProceed = true;
        public void Update()
        {
            if (!_canvas) return; // null check
            if (!_canvas.enabled) return; // dialogue canvas not enabled check
            
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                EndDialogue();
            }
            if (Input.GetKeyDown(KeyCode.Space) && _canProceed) // check for space key press and if not in cooldown
            {
                ButtonFunction();
                _canProceed = false;
                StartCoroutine(ResetCanProceed());
            }
        }
        private System.Collections.IEnumerator ResetCanProceed()
        {
            yield return new WaitForSeconds(_dialogueCooldown);
            _canProceed = true;
        }
    }
}