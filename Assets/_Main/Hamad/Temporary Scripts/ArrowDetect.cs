using UnityEngine;

public class ArrowDetect : MonoBehaviour
{

    public static bool isThere;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("UI"))
        {
            isThere = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("UI")) isThere = false;
    }

}
