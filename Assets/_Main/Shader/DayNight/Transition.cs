using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class Transition : MonoBehaviour
{

    [SerializeField] private Material transitionMaterial;

    [SerializeField] private float transitionTime;

    [SerializeField] private string propertyName = "_Progress";

    public UnityEvent OnTransitionDone;

    public void Play() 
    {
        StartCoroutine(TransitionCoroutine());
    }

    private IEnumerator TransitionCoroutine()
    {
        float currentTime = 0;
        while (currentTime < transitionTime)
        {
            currentTime += Time.deltaTime;
            transitionMaterial.SetFloat(propertyName, Mathf.Clamp01(currentTime/transitionTime));
            yield return null;
        }

        OnTransitionDone?.Invoke();
    }
    
}
