using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CustomType;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using ScriptLibrary.Singletons;
using Scripts.DayCycle;
using UnityEngine.UI;


namespace Scripts.Dialogue
{
    public class DialogueManager : Singleton<DialogueManager>
    {
        [SerializeField] private TextMeshProUGUI characterNameUI;
        [SerializeField] private TextMeshProUGUI dialogueTextUI;
        [SerializeField] private Image characterPortraitUI;
        [SerializeField] private AudioSource dialogueAudio; //added audio
        [SerializeField] private float dialogueCooldown = 0.5f;

        public static bool FreezePlayer = false;
        
        public RuntimeDialogueGraph runtimeGraph;
        
        private Dictionary<string, RuntimeDialogueNode> _nodeLookup = new();
        private RuntimeDialogueNode _currentNode;

        private RuntimeDialogueNode _nextNode;
        private DialogueInfo _currentDialogueInfo;

        private Canvas _canvas;
        private Button _button;
        
        private List<string> _lastDialogueText = new();
        
        private GameObject[] _enableAfterDialogue;
        [HideInInspector] public TriggerObjectDialogueAdder triggerObjectDialogueAdder;
        
        public void EnableThisObject(RuntimeDialogueGraph runtimeDialogueGraph, GameObject[] enableAfterDialogue)
        {
            if (PostFXManager.Instance != null) PostFXManager.Instance.BlurFX(1f);
            
            DayNightCycle.Instance?.PauseTime();

            runtimeGraph = runtimeDialogueGraph;
            _enableAfterDialogue = enableAfterDialogue;

            _canvas = GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Canvas>();
            _canvas.enabled = true;
            _button = _canvas.GetComponentInChildren<Button>();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(ButtonFunction);

            _lastDialogueText = new List<string>();

            FreezePlayer = true; //to freeze the player and prevent them from moving while talking
            
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
        private string _lastDialogueSingle = "";
        private Coroutine _runningDialogueCoroutine;
        private void ShowNode(string nodeID)
        {
            // if (_currentNode.dialogueInfo.dialogues[_counter] == _lastDialogueSingle)
            // {
            //     _counter++;
            //     ShowNode(nodeID);
            //     return;
            // }
            //
            
            var c = _currentNode.dialogueInfo.dialogues.Count;

            if (c > _counter)
            {
                if (_currentNode.dialogueInfo.dialogues[_counter].Equals(_lastDialogueSingle))
                {
                    _counter++;
                    ShowNode(nodeID);
                    return;
                }
                _lastDialogueText = new List<string>(_currentNode.dialogueInfo.dialogues);
                dialogueAudio.Play(); //added audio
                characterNameUI.text = _currentNode.dialogueInfo.characterName;
                characterPortraitUI.sprite = _currentNode.dialogueInfo.characterPortrait;
                dialogueTextUI.text = "";
                
                _fullText = _currentNode.dialogueInfo.dialogues[_counter];
                _runningDialogueCoroutine = StartCoroutine(GraduallyShowText());
                _lastDialogueSingle = _currentNode.dialogueInfo.dialogues[_counter];
                _counter++;
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
                // characterNameUI.text = _currentNode.dialogueInfo.characterName;
                // dialogueTextUI.text = _currentNode.dialogueInfo.dialogues[_counter];
                // characterPortraitUI.sprite = _currentNode.dialogueInfo.characterPortrait;
            }
        }
        private readonly float _textSpeed = 0.02f;
        private string _fullText = "";
        private string _currentlyDisplayedText = "";
        private IEnumerator GraduallyShowText()
        {
            _currentlyDisplayedText = new string(_fullText);
            foreach (char c in _fullText)
            {
                dialogueTextUI.text += c;
                yield return new WaitForSeconds(_textSpeed);
            }
        }
        private void StopGraduallyShowText()
        {
            if (_runningDialogueCoroutine != null)
            {
                StopCoroutine(_runningDialogueCoroutine);
                _runningDialogueCoroutine = null;
            }
            dialogueTextUI.text = _currentlyDisplayedText;
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
            FreezePlayer = false;
            DayNightCycle.Instance?.ResumeTime();
        }

        public void ButtonFunction()
        {
            if (!_canProceed) return; 
            _canProceed = false;
            StartCoroutine(ResetCanProceed());
            if (!string.IsNullOrEmpty(_currentNode.nextNodeID))
            {
                if (_runningDialogueCoroutine != null)
                {
                    StopGraduallyShowText();
                }

                ShowNode(_currentNode.nextNodeID);
            }
            else
            {
                EndDialogue();
            }
        }
        
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
            }
        }
        private IEnumerator ResetCanProceed()
        {
            yield return new WaitForSeconds(dialogueCooldown);
            _canProceed = true;
        }
    }
}