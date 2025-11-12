using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Unity.Mathematics;

public class FootstepSounds : MonoBehaviour
{

    

    [SerializeField] private AudioSource _playerSound;

    [SerializeField] private AudioClip[] footstepsGrass;
    [SerializeField] private AudioClip[] footstepsStone;
    [SerializeField] private AudioClip[] footstepsDirt;

    [SerializeField] private string surfaceName;

    [SerializeField] private float delay;

    [SerializeField] private LayerMask _layerMask;

    private float maxDelay;

    private Rigidbody2D _testSpeed;

    [SerializeField] private GameObject player;
    [SerializeField] private BoxCollider2D _pBox;

    void Start()
    {
        maxDelay = delay;
        _testSpeed = player.GetComponent<Rigidbody2D>();
        _pBox = player.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        

        delay -= Time.deltaTime;


            if (_testSpeed.linearVelocity.magnitude > 0f && delay <= 0)
            {
                delay = maxDelay;
                _playerSound.pitch = UnityEngine.Random.Range(0.8f, 1.15f);
                GetSurfaceType();

            switch(surfaceName)
            {

                case "Grass":
                _playerSound.PlayOneShot(footstepsGrass[UnityEngine.Random.Range(0, footstepsGrass.Length)]);
                    break;
                case "Dirt":
                    _playerSound.PlayOneShot(footstepsDirt[UnityEngine.Random.Range(0, footstepsDirt.Length)]);
                    break;
                case "Stone":
                    _playerSound.PlayOneShot(footstepsStone[UnityEngine.Random.Range(0, footstepsStone.Length)]);
                    break;
                default:
                    break;
        }

                 
            }
    }

    private void GetSurfaceType()
    {
        RaycastHit2D hit;

        hit = Physics2D.BoxCast(player.transform.position, new Vector2(1f, 1f), 0f, Vector2.up, 0f, _layerMask);

        if (hit.collider != null)
        {
            string newSurfaceTag = hit.collider.tag;

            if (hit.collider.tag != "Stone")
            {
                surfaceName = "Grass";
            }
            else
            {
                surfaceName = "Stone";
            }

        }

    }

}
