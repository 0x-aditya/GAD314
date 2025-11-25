using UnityEngine;
using System.Collections;
using UnityEngine.Rendering;
using ScriptLibrary.Singletons;

public class PostFXManager : Singleton<PostFXManager>
{
    [SerializeField] Volume blurFX;

    public void BlurFX(float weight, float duration = 0.3f)
    {
        StopAllCoroutines();
        StartCoroutine(Effect(blurFX, duration, weight));
    }

    private IEnumerator Effect(Volume volume, float duration, float weight)
    {
        if (volume == null)
            yield break;

        float startWeight = volume.weight;
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            volume.weight = Mathf.Lerp(startWeight, weight, t * t * (3f - 2f * t));

            yield return null;
        }

        volume.weight = weight;
    }
}