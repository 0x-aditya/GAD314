using UnityEngine;

public class SeedHolder : MonoBehaviour
{

    [SerializeField] private bool isOccupied;

    [SerializeField] private bool highlight;

    [SerializeField] private int seedType;

    [SerializeField] private GameObject highlightObject;

    private float time;
    private float hydration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highlightObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (highlight)
        {
            highlightObject.SetActive(true);

            //if player presses on plant
            //plants the seed
            //occupied = true;

        }
        else
        {
            highlightObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider other)
    {
        if (isOccupied)
            return;
        
        if (other.CompareTag("cursor"))
        {
            highlight = true;
        }
    }

    private void OnTriggerExit2D(Collider other)
    {
        highlight = false;
    }
}
