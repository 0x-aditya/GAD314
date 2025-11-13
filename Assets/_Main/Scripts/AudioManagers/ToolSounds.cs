using UnityEngine;

public class ToolSounds : MonoBehaviour
{

    [SerializeField] private AudioSource _playerAudio;

    [SerializeField] private AudioClip[] watercanAudio;
    [SerializeField] private AudioClip[] plantSeedAudio;
    [SerializeField] private AudioClip[] harvestPlantAudio;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPlantSeedAudio()
    {
        _playerAudio.PlayOneShot(plantSeedAudio[Random.Range(0, plantSeedAudio.Length)]);
    }
    public void PlayHarvestAudio()
    {
        _playerAudio.PlayOneShot(harvestPlantAudio[Random.Range(0, harvestPlantAudio.Length)]);
    }
    public void PlayWaterCanAudio()
    {
        _playerAudio.PlayOneShot(watercanAudio[Random.Range(0, watercanAudio.Length)]);
    }


}
