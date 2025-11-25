using Scripts.DayCycle;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Fireflies : NightObject
{
    [SerializeField] Light2D light2d;
    [SerializeField] ParticleSystem fireflies;
    [SerializeField] float fadeDuration = 1f;

    private void Awake()
    {
        ActivateNightObject(false);
    }
    public override void ActivateNightObject(bool activate)
    {
        StopAllCoroutines();
        StartCoroutine(FadeFireflies(activate));
    }

    private IEnumerator FadeFireflies(bool activate)
    {
        float startAlpha = light2d.intensity;
        float targetAlpha = activate ? 1f : 0f;
        float timer = 0f;

        if (activate && !fireflies.isPlaying) fireflies.Play();

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;
            light2d.intensity = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        light2d.intensity = targetAlpha;
        if (!activate && fireflies.isPlaying) fireflies.Stop();
    }
}