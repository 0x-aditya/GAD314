using UnityEngine;

public class CopySprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer copy; // what to copy
    [SerializeField] float brightnessAdd = 0.1f; // use to adjust original sprite brightness
    private SpriteRenderer paste;

    private void Awake()
    {
        paste = GetComponent<SpriteRenderer>();
        AddBrightness();
    }
    private void AddBrightness()
    {
        Color c = copy.color;

        copy.color = new Color(c.r + brightnessAdd, c.g + brightnessAdd, c.b + brightnessAdd, c.a);
    }
    private void Update()
    {
        paste.sprite = copy.sprite;
        paste.flipX = copy.flipX;
    }
}
