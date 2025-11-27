using UnityEngine;
using ScriptLibrary;
using Scripts.Dialogue;

public class TriggerCookingStation : OnInteractTrigger2D
{

    [SerializeField] private GameObject cookingGame;

    protected override void OnInteract()
    {
        if (cookingGame.activeSelf) cookingGame.SetActive(true);
        gameObject.SetActive(false);
    }
}
