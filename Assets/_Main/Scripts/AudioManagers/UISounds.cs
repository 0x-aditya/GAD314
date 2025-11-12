using UnityEngine;

public class UISounds : MonoBehaviour
{

    [SerializeField] private AudioSource uiSource;

    [SerializeField] private AudioClip selectSound;
    [SerializeField] private AudioClip inventorySound;
    [SerializeField] private AudioClip uiSound;
    [SerializeField] private AudioClip backSound;


    public void PlaySelectSound()
    {
        uiSource.PlayOneShot(selectSound);
    }

    public void PlayUISound()
    {
        uiSource.PlayOneShot(uiSound);
    }

    public void PlayInventorySound()
    {
        uiSource.PlayOneShot(inventorySound);
    }

    public void PlayBackSound()
    {
        uiSource.PlayOneShot(backSound);
    }

}
