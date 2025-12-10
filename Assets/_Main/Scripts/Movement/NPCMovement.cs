using UnityEngine;

public class NPCMovement : MonoBehaviour
{

    [SerializeField] private GameObject hideObject;
    [SerializeField] private GameObject showObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hideObject.SetActive(false);
        showObject.SetActive(true);
    }
}
