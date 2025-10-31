using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Unity.Mathematics;

public class FootstepSounds : MonoBehaviour
{

    [SerializeField] private AudioSource _playerSound;

    [SerializeField] private AudioClip[] footsteps;

    [SerializeField] private GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        if (footsteps != null)
        {
            _playerSound.volume = UnityEngine.Random.Range(0, footsteps.Length);
            _playerSound.pitch = UnityEngine.Random.Range(0.8f, 1.15f);
            _playerSound.PlayOneShot(footsteps[UnityEngine.Random.Range(0, footsteps.Length)]);
        }
    }
}
