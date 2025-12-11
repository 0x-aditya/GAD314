using UnityEngine;
using ScriptLibrary;
using Scripts.Dialogue;

public class TriggerCookingStation : MonoBehaviour
{

    [SerializeField] private GameObject cookingGame;
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject textThing;

    public static bool interactedWith = false;

    private void Start()
    {
        textThing.SetActive(false);
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (textThing.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            cookingGame.SetActive(true);
            textThing.SetActive(false);
            DialogueManager.FreezePlayer = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textThing.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textThing.SetActive(false);
            if (interactedWith == true)
            {
                interactedWith = false;
            }
        }
    }

}
