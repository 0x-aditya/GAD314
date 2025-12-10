using System.Collections;
using UnityEngine;
using ScriptLibrary;

public class ShakeTree : OnInteractTrigger2D
{
    private int _count = 0;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource shakeSound;
    [SerializeField] private ParticleSystem leavesParticle;
    [SerializeField] private int shakesToFinish = 10;

    [SerializeField] private float cooldownTime = 0.3f;
    private float _timeSinceLastInteract = Mathf.Infinity;

    [SerializeField] private GameObject fruitPickupObject;
    [SerializeField] private Transform fruitSpawnPoint1;
    [SerializeField] private Transform fruitSpawnPoint2;
    [SerializeField] private DisableUntilNextDay disableUntilNextDay;
    
    private bool withinCooldown => _timeSinceLastInteract > cooldownTime;
    protected override void OnInteract()
    {
        if (!withinCooldown)
            return;

        _timeSinceLastInteract = 0f;
        _count++;
        animator.SetTrigger("Shake");
        shakeSound.Play();
        leavesParticle.Play();
        if (_count >= shakesToFinish)
        {
            DropFruit();
            enabled = false;
        }
    }

    private void Update()
    {
        _timeSinceLastInteract += Time.deltaTime;
    }

    private void DropFruit()
    {

        int count = Random.Range(5, 9);
        for (int i = 0; i < count; i++)
        {
            Vector3 basePos;

            float randomValue = Random.value;
            basePos = Vector3.Lerp(fruitSpawnPoint1.position, fruitSpawnPoint2.position, randomValue);
            Vector3 spawnPos = basePos + Vector3.down * 1f;
            spawnPos += new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.2f, 0.2f), 0f);

            GameObject fruit = Instantiate(fruitPickupObject, spawnPos, Quaternion.identity);
            fruit.GetComponent<SpriteRenderer>().sortingOrder = 15;
            StartCoroutine(AnimateFallingDown(fruit.transform));
        }
        disableUntilNextDay.DisableObject();
    }

    private IEnumerator AnimateFallingDown(Transform target)
    {
        float duration = 1f;
        Vector3 dropPosition = target.position - Vector3.up * 1.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            if (!target)
                yield break;
            target.position = Vector3.Lerp(target.position, dropPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        target.position = dropPosition;

    }

}