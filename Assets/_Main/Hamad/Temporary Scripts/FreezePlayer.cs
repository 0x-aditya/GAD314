using Scripts.Dialogue;
using UnityEngine;

public class FreezePlayer : MonoBehaviour
{

    [SerializeField] private GameObject thing;
    [SerializeField] private bool isFrozen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialogueManager.FreezePlayer = isFrozen;
    }

    // Update is called once per frame
    void Update()
    {
        // if the gameobject is active

        //freeze dialogue which should freeze the player
    }
}
