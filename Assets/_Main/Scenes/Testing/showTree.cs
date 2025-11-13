using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class showTree : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("_Main/Scenes/Minigames/Juice/Juice");
        }
    }
}
