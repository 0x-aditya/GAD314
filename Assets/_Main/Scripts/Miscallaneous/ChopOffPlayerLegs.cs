using System;
using UnityEngine;

public class ChopOffPlayerLegs : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private void OnEnable()
    {
        player.GetComponent<CharacterMovementController>().enabled = false;
    }

    private void OnDisable()
    {
        player.GetComponent<CharacterMovementController>().enabled = true;
    }
}
