using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance { get; private set; }
    [SerializeField] private TextMeshProUGUI wheatText;
    private int _wheat = 0;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void AddWheat(int amount)
    {
        _wheat += amount;
        wheatText.text = "Wheat: " + _wheat;
    }
}
