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

    [SerializeField] private float delay;
    private float maxDelay;

    private Rigidbody2D _testSpeed;

    [SerializeField] private GameObject player;

    void Start()
    {
        maxDelay = delay;
        _testSpeed = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        delay -= Time.deltaTime;

        if (footsteps != null)
        {
            if (_testSpeed.linearVelocity.magnitude > 0f && delay <= 0)
            {
                delay = maxDelay;
                //_playerSound.volume = UnityEngine.Random.Range(0, footsteps.Length);
                _playerSound.pitch = UnityEngine.Random.Range(0.8f, 1.15f);
                _playerSound.PlayOneShot(footsteps[UnityEngine.Random.Range(0, footsteps.Length)]);
            }
        }
    }
}
