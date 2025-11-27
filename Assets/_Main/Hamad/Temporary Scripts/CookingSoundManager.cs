using UnityEngine;

public class CookingSoundManager : MonoBehaviour
{

    [SerializeField] private AudioClip rightHit;
    [SerializeField] private AudioClip wrongHit;
    [SerializeField] private AudioClip cookingLoop;
    [SerializeField] private AudioSource cookingLoopAudio;
    [SerializeField] private AudioClip mixing;

    public static bool loopingAudio;

    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip failure;

    [SerializeField] private AudioSource pot;

    private void Start()
    {
        loopingAudio = false;
    }

    private void Update()
    {
        if (loopingAudio)
        {
            cookingLoopAudio.Play();
        }
        else
        {
            cookingLoopAudio.Stop();
        }
    }

    public void PlayRightHit()
    {
        pot.PlayOneShot(rightHit);
    }

    public void PlayWrongHit()
    {
        pot.PlayOneShot(wrongHit);
    }

    public void PlayMixing()
    {
        pot.PlayOneShot(mixing);
    }

    public void PlaySuccess()
    {
        pot.PlayOneShot(success);
    }

    public void PlayFailure()
    {
        pot.PlayOneShot(failure);
    }


}
