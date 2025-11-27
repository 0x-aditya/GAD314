using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Minigames.Singing
{
    public class MoveNotes : MonoBehaviour
    {
        [SerializeField] private NoteInterval[] spawnNotes;
        [SerializeField] private GameObject notePrefab;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float noteSpeed = 5f;

        public float timer = 0f;
        private int _nextNoteIndex = 0;
        private List<GameObject> _activeNotes;

        private void Awake()
        {
            _activeNotes = new List<GameObject>();
        }

        private void Update()
        {
            timer += Time.deltaTime * 1000; // milliseconds

            // Check if the index is within bounds
            // Check if it's time to spawn the next note timer > spawnTime of the note
            if (_nextNoteIndex < spawnNotes.Length && timer >= spawnNotes[_nextNoteIndex].spawnTime)
            {
                SpawnNoteAt(spawnNotes[_nextNoteIndex].startPoint);
                _nextNoteIndex++; // increment to the next note
            }
            else if (_nextNoteIndex >= spawnNotes.Length)
            {
                SceneManager.LoadScene(0);
            }

            MoveAllNotesRight(); // Moves all the instantiated notes to the right
        }

        private void SpawnNoteAt(int pointIndex)
        {
            if (pointIndex < 0 || pointIndex >= spawnPoints.Length) return;

            GameObject note = Instantiate(notePrefab, spawnPoints[pointIndex].position, Quaternion.identity);
            _activeNotes.Add(note);
        }

        private void MoveAllNotesRight()
        {
            foreach (GameObject activeNote in _activeNotes)
            {
                activeNote.transform.Translate(Vector2.right * (noteSpeed * Time.deltaTime));
            }
        }
    }

    [Serializable]
    public class NoteInterval
    {
        public int startPoint;
        public int spawnTime;
    }
}