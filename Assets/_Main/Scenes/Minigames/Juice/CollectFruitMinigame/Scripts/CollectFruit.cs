using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectFruit : MonoBehaviour
{
    [SerializeField] private int fruitsToCollect = 5;
    [SerializeField] private int fruitCollected = 0;
    [SerializeField] private GameObject[] fruits;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Fruit"))
        {
            Destroy(other.gameObject);
            fruitCollected++;
        }
        if (fruitCollected >= fruitsToCollect)
        {
            SceneManager.LoadScene("CollectSuccessfull");
        }
        int c = 0;
        foreach (GameObject fruit in fruits)
        {
            c++;
            fruit.SetActive(c <= fruitCollected);
        }
    }
}
