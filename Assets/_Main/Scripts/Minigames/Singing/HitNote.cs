using System;
using UnityEngine;

namespace Minigames.Singing
{
    [RequireComponent(typeof(Collider2D))]
    public class HitNote : ScriptLibrary.Inputs.KeyPressInput
    {
        [SerializeField] private AudioClip note;
        [SerializeField] private AudioSource audioSource;

        private bool _hit = false;
        private GameObject _note;

        protected override void OnKeyDown()
        {
            audioSource.PlayOneShot(note);

            if (_hit)
            {
                Destroy(_note);
            }
            else
            {
                MusicGameManager.Instance.AddStrike();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Note"))
            {
                _hit = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Note"))
            {
                _hit = false;
            }
        }
    }
}