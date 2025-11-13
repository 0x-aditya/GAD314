using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitSpawnManager : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint1;
    [SerializeField] private Transform spawnPoint2;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxFruits = 10;
    [SerializeField] private GameObject fruitPrefab;

    private void Start()
    {
        StartCoroutine(SpawnFruits());
    }

    private IEnumerator SpawnFruits()
    {
        for (int i = 0; i < maxFruits; i++)
        {
            SpawnFruit();
            yield return new WaitForSeconds(spawnInterval);
        }
        SceneManager.LoadScene("CollectUnSuccessfull");
    }

    private void SpawnFruit()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnPoint1.position.x, spawnPoint2.position.x),
            spawnPoint1.position.y,
            spawnPoint1.position.z
        );

        Instantiate(fruitPrefab, spawnPosition, Quaternion.identity);
    }
}