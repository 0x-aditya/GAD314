using UnityEngine;

public class EnableVsync : MonoBehaviour
{
    [SerializeField] private GameObject checkMarkVsync;
    void Start()
    {
        DisplayVsync();
    }

    public void UpdateVsync()
    {
        if (QualitySettings.vSyncCount == 1)
        {
            QualitySettings.vSyncCount = 0;
        }
        else
        {
            QualitySettings.vSyncCount = 1;
        }
        DisplayVsync();
    }
    private void DisplayVsync()
    {
        if (QualitySettings.vSyncCount == 1)
        {
            checkMarkVsync.SetActive(true);
        }
        else
        {
            checkMarkVsync.SetActive(false);
        }
    }
}
